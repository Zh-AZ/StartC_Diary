using StartC_Diary;

static void Workers(string[] args)
{
    string patch = @"d:\Employees.txt";
    Workers work = new Workers(patch);
    work.ChooseFile();
}
Workers(args);
