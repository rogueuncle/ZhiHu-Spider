using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Server=127.0.0.1;Database=ZhiHu2;User Id=sa;Password=sa123456789;";

        #region 创建模型
        [Table("Answer")]
        public class Answer
        {
            public int Id { get; set; }

            [Required]
            [StringLength(32)]
            public string Author_Id { get; set; }

            [Required]
            public int Question_Id { get; set; }


        }

        #endregion

        #region 创建上下文
        public class AnswerContext : DbContext
        {
            private const string ConnectionString = Form1.ConnectionString;
            public DbSet<Answer> answers { get; set; }
            //protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
            //{
            //    //base.OnConfiguring(dbContextOptionsBuilder);
            //    dbContextOptionsBuilder.UseSqlServer(ConnectionString);
            //}

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=ZhiHu2;User Id=sa;Password=sa123456789;");
            }
        }

        #endregion

        
    }
}
