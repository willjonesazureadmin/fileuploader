using System.Collections.Specialized;

namespace fileuploader.common.helpers;

public static class QueryStringBuilder
{
    public static NameValueCollection Build(NameValueCollection? nameValueCollection, params string[] data)
    {
        try
        {
            if (nameValueCollection == null)
            {
                nameValueCollection = System.Web.HttpUtility.ParseQueryString(string.Empty);
                Console.WriteLine("Creating nvc");

            }
            int i = 0;
            while (i < data.Count())
            {
                if (data[i] != null)
                {
                    nameValueCollection.Add(data[i + 1].ToString(), data[i].ToString());

                }
                i += 2;
            }

            return nameValueCollection;
        }
        catch
        {
            Console.WriteLine("This didnt work");
            throw;
        }

    }
}