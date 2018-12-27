Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Collections
Imports System.IO

Namespace Examples.SmtpExamples.Sync
    Public Class CtorExamples
        '<snippet6>
        Sub CreateMessageWithAttachment(ByVal server As String)
            ' Specify the file to be attached And sent.
            ' This example assumes that a file named Data.xls exists in the
            ' current working directory.
            Dim file As String = "data.xls"

            ' Create a message and set up the recipients.
            Dim message As New MailMessage("jane@contoso.com", "ben@contoso.com", "Quarterly data report.", "See the attached spreadsheet.")

            ' Create  the file attachment for this email message.
            Dim data As New Attachment(file, MediaTypeNames.Application.Octet)

            ' Add time stamp information for the file.
            data.ContentDisposition.CreationDate = System.IO.File.GetCreationTime(file)
            data.ContentDisposition.ModificationDate = System.IO.File.GetLastWriteTime(file)
            data.ContentDisposition.ReadDate = System.IO.File.GetLastAccessTime(file)

            ' Add the file attachment to this email message.
            message.Attachments.Add(data)

            ' Send the message.
            Dim client As New SmtpClient(server)

            ' Add credentials if the SMTP server requires them.
            client.Credentials = CredentialCache.DefaultNetworkCredentials
            client.Send(message)

            ' Display the values in the ContentDisposition for the attachment.
            Console.WriteLine("Content disposition")
            Console.WriteLine(data.ContentDisposition.ToString())
            Console.WriteLine("File {0}", data.ContentDisposition.FileName)
            Console.WriteLine("Size {0}", data.ContentDisposition.Size)
            Console.WriteLine("Creation {0}", data.ContentDisposition.CreationDate)
            Console.WriteLine("Modification {0}", data.ContentDisposition.ModificationDate)
            Console.WriteLine("Read {0}", data.ContentDisposition.ReadDate)
            Console.WriteLine("Inline {0}", data.ContentDisposition.Inline)
            Console.WriteLine("Parameters: {0}", data.ContentDisposition.Parameters.Count)
            For Each d As DictionaryEntry In data.ContentDisposition.Paramters
                Console.WriteLine("{0} = {1}", d.Key, d.Value)
            Next

            data.Dispose()
        End Sub
        '</snippet6>
    End Class
End Namespace