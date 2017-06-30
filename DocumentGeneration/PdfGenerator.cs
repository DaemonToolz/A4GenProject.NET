using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration{
    public class PdfGenerator{
        public Dictionary<String, String> Changes { get; private set; }
        public String TargetName { get; private set; }
        public String Template { get; private set; }

        public PdfGenerator(Dictionary<String,String> ChangesToApply, String FriendlyName, String TemplatePath){
            Changes = ChangesToApply;
            TargetName = FriendlyName;
            Template = TemplatePath;
        }

        public void DeleteFile(){
            try
            {
                File.Delete(TargetName);
            }
            catch{

            }
        }

        public void GenerateAnalysisReport(){
            Application word = new Application() { Visible = false };
            var Document = word.Documents.Open(Template, Visible: false, ReadOnly: false);
            Document.Activate();

            foreach (var key in Changes.Keys)
                word.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll, FindText: key, ReplaceWith: Changes[key]);


            // Exportation en format PDF
            Document.ExportAsFixedFormat(
                        TargetName,
                        WdExportFormat.wdExportFormatPDF,
                        OptimizeFor: WdExportOptimizeFor.wdExportOptimizeForOnScreen,
                        BitmapMissingFonts: true, DocStructureTags: false);

            // Annule les changements sur le document d'origine
            foreach (var key in Changes.Keys)
                word.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll, FindText: Changes[key], ReplaceWith: key);

            Document.Save();
            Document.Close();
        }
    }
}
