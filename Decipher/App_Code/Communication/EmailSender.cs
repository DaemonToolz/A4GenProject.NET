using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
//using DocumentGeneration;
using System.Net.Mime;

/// <summary>
/// Summary description for SendEmail
/// </summary>
public static class  EmailSender
{
    public static void SendEmail(String email, String content, String Filename, String TrustIndex = "Unknown", String Key = "Unknown", String TargetIdentity = "Unknown")
    {
        /*
        Dictionary<String, String> ChangesToApply = new Dictionary<String, String>();
        ChangesToApply.Add("%SenderEmail%", "axel.maciejewski@viacesi.fr");
        ChangesToApply.Add("%ProvidedEmail%", email);
        ChangesToApply.Add("%TestedFile%", Filename);
        ChangesToApply.Add("%DecipheredText%", content);
        ChangesToApply.Add("%CipheringKey%", Key);
        ChangesToApply.Add("%TrustIndex%", TrustIndex);

        PdfGenerator Generator = new PdfGenerator(ChangesToApply, @"C:\inetpub\ProjectLogs\Document_" + email.GetHashCode() + ".pdf", @"C:\inetpub\ProjectLogs\TemplateRapportConfiance.docx");

        Generator.GenerateAnalysisReport();
        */
        String from = "axel.maciejewski@viacesi.fr",
            to = email,
            subject = "White Hat & Co. - Résultat analyse cryptographique du fichier " + Filename,
            body = "L'analyse du fichier s'est terminée avec succès." +
                    Environment.NewLine + "Voici l'identité de la cible : " + TargetIdentity +
                    Environment.NewLine + "Voici le contenu déchiffré : " + content +
                    Environment.NewLine + "Voici la clé de chiffrement / déchiffrement " + Key +
                    Environment.NewLine + "Le pourcentage de confiance est : " + TrustIndex;

        MailMessage mail = new MailMessage();
        mail.IsBodyHtml = false;
        mail.To.Add(to);
        mail.From = new MailAddress(from, "Email head", System.Text.Encoding.UTF8);
        mail.Subject = subject;
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = body;
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        // Non fonctionnel

        /*
        Attachment attachment = new Attachment(Generator.TargetName, MediaTypeNames.Application.Octet);
        ContentDisposition disposition = attachment.ContentDisposition;
        disposition.CreationDate = System.IO.File.GetCreationTime(Generator.TargetName);
        disposition.ModificationDate = System.IO.File.GetLastWriteTime(Generator.TargetName);
        disposition.ReadDate = System.IO.File.GetLastAccessTime(Generator.TargetName);
        disposition.FileName = System.IO.Path.GetFileName(Generator.TargetName);
        disposition.Size = new System.IO.FileInfo(Generator.TargetName).Length;
        disposition.DispositionType = DispositionTypeNames.Attachment;
        mail.Attachments.Add(attachment);
        */
        using (SmtpClient smtp = new System.Net.Mail.SmtpClient())
        {
            smtp.Credentials = new System.Net.NetworkCredential(from, "MOT DE PASSE ICI"); // tempo
            //smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Port = 25;
            smtp.Host = "smtp.outlook.com"; // "outlook"
            smtp.EnableSsl = true;
            smtp.Timeout = 20000;
            smtp.Send(from, to, subject, body);
        }


        //Generator.DeleteFile();
    }
}