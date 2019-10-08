using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Components;

namespace MovieQuestion.Client.Utils
{
    public static class Extensions
    {
        public static ValueTask<T> GetItem<T>(this LocalStorage storage)
        {
            return storage.GetItem<T>(typeof(T).Name);
        }

        public static void Navigate(this NavigationManager manager, string path)
        {
            manager.NavigateTo($"{path}");
        }
    }
}
