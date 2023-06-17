namespace ClassLibrary
{
    public class CSVFile
    {
        //List that holds Infix expressions
        public List<string[]> InFix;
        public CSVFile()
        {
            InFix = new List<string[]>();
        }

        //Deserialize the csv file and put the data in the list
        public void CSVDeserialize(string csvPath)
        {
            using (var reader = new StreamReader(csvPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    if (values[0] != "sno" && values[1] != "infix")
                    {
                        InFix.Add(values);
                    }
                }
            }
        }
    }
}