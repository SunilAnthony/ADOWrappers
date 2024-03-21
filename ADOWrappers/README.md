# ADO Wrappers


## Description

> This nuget package contain many util, extensions and etc to help developer speed up their coding process.


## SQL Data Reader & SQL DataTable

> This is a nuget package which will allow you to simpily map a Sql Data Reader to any class. It is a generic class that will return a list of <T> where T: is a class. 
> Simpily register the package in your startup.

```
builder.Services.UseSqlDataReaderMapper();

```

Or you can directly instantiate the generic classes as they are not an abstract class. 

> To use in any of your class, inject the interface in your constructor.



```
public class MyClass
{
	private readonly IGenericSqlDataReader _genericSqlReader;
	public MyClass(IGenericSqlDataReader genericSqlReader)
	{
		_genericSqlReader = genericSqlReader;
	}

	public async List<MyOjbect> GetListOfObject(string param1)
	{
		//Parameter are optional
		SqlParameter[] parameters = new SqlParameter[]
		{
			new SqlParameter("@ParamName", SqlDbType.VarChar, 12) { Value = param1 },
			// Add other parameters as needed
		};
		return await _genericSqlReader.ExecuteReaderAsync<MyOjbect>("connectionString", "myStoreProcedure", CommandType.StoredProcedure, parameters );
	}

}


```
Paramaters:
1. Connection String
2. Store Procedure Name or Sql Query 
3. Command Type. (CommandType.Text || CommandType.StoreProcedure) depending on what you're pass for param 2
4. This is an optional if your query or Store Procedure require Parameter's. 

### Notes

There is also an synchronous method if you don't need an async method. 
> In addition, there is extension to map your "SqlDataReader" object to a class(No dependancy Registration Required).
For Example:

```
using (var dataReader = sqlCommand.ExecuteReader(CommandBehavior.Default))
{
	if (dataReader.HasRows)
	{
		while (dataReader.Read())
		{
			var newObject = new T();
			dataReader.MapDataToObject(newObject);
			newListObject.Add(newObject);
		}
	}
}

```



You can also used the mapping extension for your "DataTable".

```
using (var dataTable = new SqlDataAdapter(sqlCommand))
{
	DataTable dt = new();
	dataTable.Fill(dt);
	if (dt.Rows.Count > 0)
	{
		newListObject = dt.ConvertDataTable<T>();
	}
}

```





This package uses the package FastMember

Free to use

Built for .net 8

> Created By [Sunil Anthony](https://www.sunilanthony.com)
