using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows.Forms;


namespace Views
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Server=127.0.0.1;Database=WroxBooks;User Id=sa;Password=sa123456789;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var conn = new AnswerContext())
            {
                //conn.Database.EnsureCreated();
                //conn.Database.Migrate();
                var x = conn.answers.ToList();
            }
        }

        [Table("Ans")]
        public class Answer
        {
            public int Id { get; set; }
            public string name { get; set; }

            [Required]
            [StringLength(32)]
            public string Author_Id { get; set; }

            [Required]
            public int Question_Id { get; set; }
        }

        public class AnswerContext : DbContext
        {
            private const string ConnectionString = Form1.ConnectionString;
            public DbSet<Answer> answers { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
            {
                base.OnConfiguring(dbContextOptionsBuilder);
                dbContextOptionsBuilder.UseSqlServer(ConnectionString);
            }
        }


    }
}
