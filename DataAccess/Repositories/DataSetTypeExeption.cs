namespace DataAccess.Repository;

public class DataSetTypeExeption:Exception
{
    public DataSetTypeExeption(string message, Exception inner)
        : base(message, inner)
    {
        
    }
   

   
}


