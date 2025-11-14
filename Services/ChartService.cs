using Microsoft.Extensions.Options;
using ModustaAPI.Models;
using ModustaAPI.Tools;
using MongoDB.Driver;
using System.Data;

namespace ModustaAPI.Services
{
    public class ChartService
    {
        private readonly CurriculumService _dataService;
        public ChartService(CurriculumService dataService)
        {
            _dataService = dataService;
        }

        public async Task<ChartBarData?> GetChartAsync(string id)
        {
            var cvs = await _dataService.GetAsync(id);
            if (cvs == null) { return null; }
            var fmCount = cvs.Clients.Select(x => x.Environnement.Framework.Count).ToArray();
            var entrepriseCount = cvs.Clients.Select(x => x.Entreprise).ToArray();

            var dsFm = new BarDataset
            {
                BackgroundColor = "#ff5733",
                Label = "Frameworks",
                Data = fmCount.ToArray(),
            };
            var result = new ChartBarData { Labels = entrepriseCount.ToArray() };

            return result;
        }

        public ChartPieData GetPieChartDataForFrameworkAsync(Curriculum cvs)
        {
            var allFmks = cvs.Clients.SelectMany(c => c.Environnement.Framework)
                          .GroupBy(fm => fm.Trim().ToLowerInvariant())
                          .Select(g => new { Nom = g.Key, Count = g.Count() })
                          .ToList();

            var result = new List<ChartDonutData>();
   
            foreach (var fmk in allFmks)
            {
                var tmp =new  ChartDonutData();
                tmp.Label=(fmk.Nom);
                tmp.Nombre = (fmk.Count);
                tmp.Couleur = (ColorGenerator.GenerateRandomColor());
                result.Add(tmp);
            }


            var chartData = new ChartPieData
            {
                Labels = result.Select(x => x.Label).ToList(), 
                Datasets = new List<PieDataSet>
                {
                new PieDataSet
                {
                    Label = "Frameworks",
                    Data =  result.Select(x => x.Nombre).ToList(),
                    BackgroundColor =  result.Select(x => x.Couleur).ToList(),
                    HoverOffset = 4
                }
            }
            };

            return chartData;
        }

        public ChartPieData GetPieChartDataForLogicielsAsync(Curriculum cvs)
        {
            var allFmks = cvs.Clients.SelectMany(c => c.Environnement.Logiciels)
                          .GroupBy(fm => fm.Trim().ToLowerInvariant())
                          .Select(g => new { Nom = g.Key, Count = g.Count() })
                          .ToList();

            var result = new List<ChartDonutData>();

            foreach (var fmk in allFmks)
            {
                var tmp = new ChartDonutData();
                tmp.Label = (fmk.Nom);
                tmp.Nombre = (fmk.Count);
                tmp.Couleur = (ColorGenerator.GenerateRandomColor());
                result.Add(tmp);
            }


            var chartData = new ChartPieData
            {
                Labels = result.Select(x => x.Label).ToList(),
                Datasets = new List<PieDataSet>
                {
                new PieDataSet
                {
                    Label = "Logiciels",
                    Data =  result.Select(x => x.Nombre).ToList(),
                    BackgroundColor =  result.Select(x => x.Couleur).ToList(),
                    HoverOffset = 4
                }
            }
            };

            return chartData;
        }

        public ChartPieData GetPieChartDataForBddsAsync(Curriculum cvs)
        {
            var allFmks = cvs.Clients.SelectMany(c => c.Environnement.Bdds)
                          .GroupBy(fm => fm.Trim().ToLowerInvariant())
                          .Select(g => new { Nom = g.Key, Count = g.Count() })
                          .ToList();

            var result = new List<ChartDonutData>();

            foreach (var fmk in allFmks)
            {
                var tmp = new ChartDonutData();
                tmp.Label = (fmk.Nom);
                tmp.Nombre = (fmk.Count);
                tmp.Couleur = (ColorGenerator.GenerateRandomColor());
                result.Add(tmp);
            }


            var chartData = new ChartPieData
            {
                Labels = result.Select(x => x.Label).ToList(),
                Datasets = new List<PieDataSet>
                {
                new PieDataSet
                {
                    Label = "Bases de données",
                    Data =  result.Select(x => x.Nombre).ToList(),
                    BackgroundColor =  result.Select(x => x.Couleur).ToList(),
                    HoverOffset = 4
                }
            }
            };

            return chartData;
        }

        public ChartPieData GetPieChartDataForIdeAsync(Curriculum cvs)
        {
            var allFmks = cvs.Clients.SelectMany(c => c.Environnement.Ide)
                          .GroupBy(fm => fm.Trim().ToLowerInvariant())
                          .Select(g => new { Nom = g.Key, Count = g.Count() })
                          .ToList();

            var result = new List<ChartDonutData>();

            foreach (var fmk in allFmks)
            {
                var tmp = new ChartDonutData();
                tmp.Label = (fmk.Nom);
                tmp.Nombre = (fmk.Count);
                tmp.Couleur = (ColorGenerator.GenerateRandomColor());
                result.Add(tmp);
            }


            var chartData = new ChartPieData
            {
                Labels = result.Select(x => x.Label).ToList(),
                Datasets = new List<PieDataSet>
                {
                new PieDataSet
                {
                    Label = "IDE",
                    Data =  result.Select(x => x.Nombre).ToList(),
                    BackgroundColor =  result.Select(x => x.Couleur).ToList(),
                    HoverOffset = 4
                }
            }
            };

            return chartData;
        }

        public ChartPieData GetPieChartDataForLanguagesAsync(Curriculum cvs)
        {
            var allFmks = cvs.Clients.SelectMany(c => c.Environnement.Languages)
                          .GroupBy(fm => fm.Trim().ToLowerInvariant())
                          .Select(g => new { Nom = g.Key, Count = g.Count() })
                          .ToList();

            var result = new List<ChartDonutData>();

            foreach (var fmk in allFmks)
            {
                var tmp = new ChartDonutData();
                tmp.Label = (fmk.Nom);
                tmp.Nombre = (fmk.Count);
                tmp.Couleur = (ColorGenerator.GenerateRandomColor());
                result.Add(tmp);
            }


            var chartData = new ChartPieData
            {
                Labels = result.Select(x => x.Label).ToList(),
                Datasets = new List<PieDataSet>
                {
                new PieDataSet
                {
                    Label = "Langages",
                    Data =  result.Select(x => x.Nombre).ToList(),
                    BackgroundColor =  result.Select(x => x.Couleur).ToList(),
                    HoverOffset = 4
                }
            }
            };

            return chartData;
        }

        public ChartPieData GetPieChartDataForAutreAsync(Curriculum cvs)
        {
            var allFmks = cvs.Clients.SelectMany(c => c.Environnement.Autre)
                          .GroupBy(fm => fm.Trim().ToLowerInvariant())
                          .Select(g => new { Nom = g.Key, Count = g.Count() })
                          .ToList();

            var result = new List<ChartDonutData>();

            foreach (var fmk in allFmks)
            {
                var tmp = new ChartDonutData();
                tmp.Label = (fmk.Nom);
                tmp.Nombre = (fmk.Count);
                tmp.Couleur = (ColorGenerator.GenerateRandomColor());
                result.Add(tmp);
            }


            var chartData = new ChartPieData
            {
                Labels = result.Select(x => x.Label).ToList(),
                Datasets = new List<PieDataSet>
                {
                new PieDataSet
                {
                    Label = "Autres",
                    Data =  result.Select(x => x.Nombre).ToList(),
                    BackgroundColor =  result.Select(x => x.Couleur).ToList(),
                    HoverOffset = 4
                }
            }
            };

            return chartData;
        }

          public ChartBarData GetPieChartDataForTest(Curriculum cv)
          {
              var frameworkCount = cv.Clients.Select(x => x.Environnement.Framework.Count).ToArray();
              var bddsCount = cv.Clients.Select(x => x.Environnement.Bdds.Count).ToArray();
              var ideCount = cv.Clients.Select(x => x.Environnement.Ide.Count).ToArray();
              var langagesCount = cv.Clients.Select(x => x.Environnement.Languages.Count).ToArray();
              var logicielsCount = cv.Clients.Select(x => x.Environnement.Logiciels.Count).ToArray();
              var autreCount = cv.Clients.Select(x => x.Environnement.Autre.Count).ToArray();
              var entrepriseCount = cv.Clients.Select(x => x.Entreprise).ToArray();

              var dsFm = new BarDataset
              {
                  BackgroundColor = "#aa5733",
                  Label = "Frameworks",
                  Data = frameworkCount.ToArray(),
              };
              var dsBdd = new BarDataset
              {
                  BackgroundColor = "#005733",
                  Label = "BDD",
                  Data = bddsCount.ToArray(),
              };
              var dsIDe = new BarDataset
              {
                  BackgroundColor = "#ff5799",
                  Label = "IDE",
                  Data = ideCount.ToArray(),
              };
              var dsLang = new BarDataset
              {
                  BackgroundColor = "#ff8899",
                  Label = "Langages",
                  Data = langagesCount.ToArray(),
              };
              var dsLogi = new BarDataset
              {
                  BackgroundColor = "#008833",
                  Label = "Logiciels",
                  Data = logicielsCount.ToArray(),
              };
              var dsAutre = new BarDataset
              {
                  BackgroundColor = "#ffccaa",
                  Label = "Autre",
                  Data = autreCount.ToArray(),
              };
              var result = new ChartBarData { Labels = entrepriseCount.ToArray(), Datasets = new List<BarDataset>() };
              result.Datasets.Add(dsFm);
              result.Datasets.Add(dsBdd);
              result.Datasets.Add(dsIDe);
              result.Datasets.Add(dsLang);
              result.Datasets.Add(dsLogi);
              result.Datasets.Add(dsAutre);
              return result;
          }
    }
}
