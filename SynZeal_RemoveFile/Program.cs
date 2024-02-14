// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var directoryPath = "F:\\Working\\Synzeal_Quotation\\Synzeal_Inventory\\Content\\Quotation\\";
//var directoryPath = @"D:\\Plesk\\Vhosts\\synzeal.com\\spms.synzeal.com\\Content\\Quotation\\";
DirectoryInfo d = new DirectoryInfo(directoryPath); //Assuming Test is your Folder
FileInfo[] Files = d.GetFiles("*.pdf"); //Getting Text files

var newprodirectoryPath = "F:\\Working\\Synzeal_Quotation\\Synzeal_Inventory\\Content\\NewProducts\\";
//var newprodirectoryPath = @"D:\\Plesk\\Vhosts\\synzeal.com\\spms.synzeal.com\\Content\\\NewProducts\\";
DirectoryInfo newprod = new DirectoryInfo(newprodirectoryPath); //Assuming Test is your Folder
FileInfo[] newproFiles = newprod.GetFiles("*.pdf"); //Getting Text files

//var attachmentdirectoryPath = "D:\\Plesk\\Vhosts\\synzeal.com\\spms.synzeal.com\\Content\\Attachment\\";
//DirectoryInfo attachment = new DirectoryInfo(attachmentdirectoryPath); //Assuming Test is your Folder
//FileInfo[] attachmentFiles = attachment.GetFiles("*.pdf"); //Getting Text files

string str = "";
string format = "Mddyyyyhhmmss";
DateTime date = DateTime.Now.AddDays(-15);
Console.WriteLine(directoryPath);
var ms = 1;
while (DateTime.Now > date.AddSeconds(ms))
{
    string matchdate = date.ToString(format);
    Console.WriteLine("Process For Date " + matchdate);
    foreach (FileInfo file in Files)
    {
        //Console.WriteLine("Path : " + directoryPath + "" + file.Name);
        if (file.Name.Contains("PO_No_Order-"))
        {
            Console.WriteLine("PO_No_Order " + file.Name);
            System.IO.File.Delete(directoryPath + "" + file.Name);
        }
        if (file.Name.Contains("mastercoa-"))
        {
            Console.WriteLine("mastercoa " + file.Name);
            System.IO.File.Delete(directoryPath + "" + file.Name);
        }
        if (file.Name.Contains("testcoa-"))
        {
            Console.WriteLine("testcoa " + file.Name);
            System.IO.File.Delete(directoryPath + "" + file.Name);
        }
        if (file.Name.Contains("SamplerepresentativeCOA_"))
        {
            Console.WriteLine("SamplerepresentativeCOA_ " + file.Name);
            System.IO.File.Delete(directoryPath + "" + file.Name);
        }
        
        if (file.Name.Contains(matchdate))
        {
            str = str + ", " + file.Name;
            Console.WriteLine("Mached file name for the date is " + file.Name);
            System.IO.File.Delete(directoryPath + "" + file.Name);
        }
    }

    foreach (FileInfo file in newproFiles)
    {
        if (file.Name.Contains(matchdate))
        {
            str = str + ", " + file.Name;
            Console.WriteLine("New Products Mached file name for the date is " + file.Name);
            System.IO.File.Delete(directoryPath + "" + file.Name);
        }
    }

    //foreach (FileInfo file in attachmentFiles)
    //{
    //    if (file.Name.Contains(matchdate))
    //    {
    //        str = str + ", " + file.Name;
    //        Console.WriteLine("Attachment File Deleted " + file.Name);
    //        System.IO.File.Delete(directoryPath + "" + file.Name);
    //    }
    //}
    

    date = date.AddSeconds(ms);
}
Environment.Exit(0);



