using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebApp.Models
{
    public sealed class DataIndexManager
    {
        private static readonly DataIndexManager DataManger = new DataIndexManager();
        Queue<BookSaveModel> queueSaveBook = new Queue<BookSaveModel>();
        private DataIndexManager()
        {

        }
        public static DataIndexManager GetInstance()
        {
            return DataManger;
        }
        //将数据插入队列之中
        public void InserData2Queue(int id, string title, string content, EnumBookSave enumBook)
        {
            BookSaveModel book = new BookSaveModel() { ID = id, Title = title, ContentDescription = content, EnumSaveBook = enumBook };
            queueSaveBook.Enqueue(book);
        }
        //开启线程扫描队列
        public void ThreadScanBookQueue()
        {
            ThreadPool.QueueUserWorkItem(a =>
            {
                while (true)
                {
                    if (queueSaveBook.Count() > 0)
                    {
                        //  BookSaveModel book = queueSaveBook.Dequeue();

                        SavaBook2Lucene();

                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }
            });
        }

        private void SavaBook2Lucene()
        {


            string indexPath = @"D:\lucenedir";//注意和磁盘上文件夹的大小写一致，否则会报错。将创建的分词内容放在该目录下。
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());//指定索引文件(打开索引目录) FS指的是就是FileSystem
            bool isUpdate = IndexReader.IndexExists(directory);//IndexReader:对索引进行读取的类。该语句的作用：判断索引库文件夹是否存在以及索引特征文件是否存在。
            if (isUpdate)
            {
                //同时只能有一段代码对索引库进行写操作。当使用IndexWriter打开directory时会自动对索引库文件上锁。
                //如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁（提示一下：如果我现在正在写着已经加锁了，但是还没有写完，这时候又来一个请求，那么不就解锁了吗？这个问题后面会解决）
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isUpdate, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);//向索引库中写索引。这时在这里加锁。                   
                                                                                                                                                   //  string txt = File.ReadAllText(@"D:\传智讲课\0413班\OA\OA项目，第七天搜索，Lucene.Net，盘古分词，Quartz.Net\资料\测试文件\" + i + ".txt", System.Text.Encoding.Default);//注意这个地方的编码
            while (queueSaveBook.Count > 0)
            {
                BookSaveModel book = queueSaveBook.Dequeue();
                writer.DeleteDocuments(new Term("ID", book.ID.ToString()));
                if (book.EnumSaveBook == EnumBookSave.DeleteBook)
                {
                    continue;
                }
                Document document = new Document();//表示一篇文档。
                                                   //Field.Store.YES:表示是否存储原值。只有当Field.Store.YES在后面才能用doc.Get("number")取出值来.Field.Index. NOT_ANALYZED:不进行分词保存
                document.Add(new Field("ID", book.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                //Field.Index. ANALYZED:进行分词保存:也就是要进行全文的字段要设置分词 保存（因为要进行模糊查询）
                document.Add(new Field("title", book.Title, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                //Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS:不仅保存分词还保存分词的距离。
                document.Add(new Field("body", book.ContentDescription, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                writer.AddDocument(document);
                writer.Close();//会自动解锁。
                directory.Close();//不要忘了Close，否则索引结果搜不到
                //return Content("OK");
            }
        }
    }
}