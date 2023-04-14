namespace TvShowSite.Core.ExtensionMethods
{
    public static class IsNullOrDefaultExtension
    {
        public static bool IsNullOrDefault<T>(this T argument)
        {
            if(argument == null)
                return true;
            if (object.Equals(argument, default(T)))
                return true;

            // deal with non-null nullables
            Type methodType = typeof(T);

            if (Nullable.GetUnderlyingType(methodType) != null) 
                return false;

            Type argumentType = argument.GetType();

            if (argumentType.IsValueType && argumentType != methodType)
            {
                object? obj = Activator.CreateInstance(argument.GetType());

                return obj == null ? true : obj.Equals(argument);
            }

            return false;
        }
    }
}
