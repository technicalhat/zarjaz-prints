using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Data;
using Nest;

namespace Indexer
{
    class Program
    {
        static void Main(string[] args)
        {
            //right, the indexer needs to:
            //starting from a defined folder, read every .json file
            //push the json object into a defined es index 
            //do I need to do anything with the images, or am I just serving them from the folder they live in
            //yeah, that's probably easiest - just add a /images redirect into the nancy app pointing at whatever folder
            //obviously, that'll need modifying for the live version since I'll be using S3

            var endpoint = "http://localhost:9200";
            var index = "zarjaz.progs";
            var datasource = "D:\\Development\\web\\ZarjazPrints\\extractor\\out";

            var search = new ElasticClient(new ConnectionSettings
                (
                    new Uri(endpoint),
                    defaultIndex: index
                )
            );
            search.DeleteIndex(index);

            search.CreateIndex(i =>
                i.Index(index)
                .Analysis(a=>
                    a.Analyzers(
                        an=>
                        an.Add("standard", new StandardAnalyzer())
                        .Add("keywordfield", new CustomAnalyzer
                        {
                            Tokenizer = "keyword"
                            
                        })
                    )
                )
                .AddMapping<ProgInfo>(
                    m=> m.Properties(
                        p=> p.String(s=> s.Name("cover").IndexAnalyzer("keywordfield").SearchAnalyzer("keywordfield"))
                        .String(s=> s.Name("artist").IndexAnalyzer("keywordfield").SearchAnalyzer("keywordfield"))
                    )
                )
            );
            foreach(var file in Directory.GetFiles(datasource, "*.json"))
            {
                using (var stream = File.OpenText(file))
                {
                    Console.WriteLine(file);
                    var prog = JsonConvert.DeserializeObject<ProgInfo>(stream.ReadToEnd());

                    search.Index(prog);

                }

            }

            Console.ReadLine();
        }
    }
}
