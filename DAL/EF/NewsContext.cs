using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.EF
{
    public class NewsContext:DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<NewsSource> NewsSources { get; set; }
        public DbSet<Rubric> Rubrics { get; set; }

        static NewsContext()
        {
            Database.SetInitializer<NewsContext>(new NewsDbInitializer());
        }

        public NewsContext(string connectionString) : base(connectionString) { }
    }

    public class NewsDbInitializer : DropCreateDatabaseAlways<NewsContext>
    {
        protected override void Seed(NewsContext context)
        {
            Rubric rubric1 = new Rubric
            {
                Id = 1,
                NameRubric = "Экономика",
            };
            Rubric rubric2 = new Rubric
            {
                Id = 2,
                NameRubric = "Политика",
            };
            Rubric rubric3 = new Rubric
            {
                Id = 3,
                NameRubric = "Спорт",
            };

            NewsSource newsSource1 = new NewsSource
            {
                Id = 1,
                NameSourceNews = "Podrobosti",
            };
            NewsSource newsSource2 = new NewsSource
            {
                Id = 2,
                NameSourceNews = "Ictv",
            };
            NewsSource newsSource3 = new NewsSource
            {
                Id = 3,
                NameSourceNews = "5 канал",
            };

            News news1 = new News
            {
                Id = 1,
                TitleNews = "Верховный суд США приостановил судебный запрет по",
                PreviewNews = "Верховный суд США приостановил судебный запрет по иммиграционному указу президента Дональда Трампа. Решение опубликовано на сайте суда. В документе говорится о том, что Верховный суд разрешил действие указа до по крайней мере 12.00 12 сентября(19.00 по Киеву).",
                FullNews = "1 opCreateDatabaseIfModelChanges: данный инициализатор проверяет на соответствие моделям определения таблиц в базе данных. И если модели не соответствуют определению таблиц, то база данные пересоздается",
                CreateDateTimeNews = DateTime.Now,
                StringImage = "/Images/06334563-bc44-4f8e-b9cc-c725da946172.jpeg",

                Rubric = rubric1,
                NewsSource = newsSource1
            };
            News news2 = new News
            {
                // Id = 2,
                TitleNews = "Президент УЕФА Александер Чеферин заявил,",
                PreviewNews =
                    "Президент УЕФА Александер Чеферин заявил, что готов исключить ПСЖ из еврокубков, если будет доказана вина парижского клуба в нарушении финансового фейр-плей. Об этом он рассказал в интервью L'Equipe.Никто не может быть выше закона. В ближайшее время мы рассмотрим результаты расследования трансферов парижан и санкции будут самыми строгими. Если вина ПСЖ будет доказана, я исключу ПСЖ из еврокубков. Мы не боимся наказывать,-подчеркнул словенец.",
                FullNews =
                    "2 opCreateDatabaseIfModelChanges: данный инициализатор проверяет на соответствие моделям определения таблиц в базе данных. И если модели не соответствуют определению таблиц, то база данные пересоздается",
                CreateDateTimeNews = DateTime.Now,
                StringImage = "/Images/8a33a883-3d7b-4917-b3d5-645b7fc422ad.jpeg",
                Rubric = rubric3,
                NewsSource = newsSource2
            };
            News news3 = new News
            {
                //  Id = 3,
                TitleNews = "В США парк Буш Гарден эвакуирова",
                PreviewNews = "В США парк Буш Гарден эвакуировал фламинго из-за урагана Ирмы, обрушившегося на штат. Видео эвакуации птиц поделился телеканал CNN.В целом планировалось спасать 12 тысяч особей.Судя по информации, обнародованной телеканалом, в настоящее время птицам ничего не угрожает.",
                FullNews = "3 opCreateDatabaseIfModelChanges: данный инициализатор проверяет на соответствие моделям определения таблиц в базе данных. И если модели не соответствуют определению таблиц, то база данные пересоздается",
                CreateDateTimeNews = DateTime.Now,
                StringImage = "/Images/e59e1294-1c2f-4546-bf0b-8a511275c0bd.jpeg",
                Rubric = rubric2,
                NewsSource = newsSource1
            };
            News news4 = new News
            {
                //  Id = 4,
                TitleNews = "Видео эвакуации птиц поделился телеканал CNN.В целом планировалось спасать 12 тысяч особей.Судя по информации, обнародованной телеканалом",
                PreviewNews = "В США парк Буш Гарден эвакуировал фламинго из-за урагана Ирмы, обрушившегося на штат. Видео эвакуации птиц поделился телеканал CNN.В целом планировалось спасать 12 тысяч особей.Судя по информации, обнародованной телеканалом, в настоящее время птицам ничего не угрожает.",
                FullNews = "4 opCreateDatabaseIfModelChanges: данный инициализатор проверяет на соответствие моделям определения таблиц в базе данных. И если модели не соответствуют определению таблиц, то база данные пересоздается",
                CreateDateTimeNews = DateTime.Now,
                StringImage = "/Images/8a33a883-3d7b-4917-b3d5-645b7fc422ad.jpeg",
                Rubric = rubric2,
                NewsSource = newsSource3
            };
            context.Rubrics.Add(rubric1);
            context.Rubrics.Add(rubric2);
            context.Rubrics.Add(rubric3);
        
            context.NewsSources.Add(newsSource1);
            context.NewsSources.Add(newsSource2);
            context.NewsSources.Add(newsSource3);

            context.News.Add(news1);
            context.News.Add(news2);
            context.News.Add(news3);
            context.News.Add(news4);
            context.SaveChanges();
        }
    }
}
