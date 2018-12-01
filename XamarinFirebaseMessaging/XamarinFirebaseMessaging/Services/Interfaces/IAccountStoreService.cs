using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFirebaseMessaging.Services.Interfaces
{
    public interface IAccountStoreService
    {
        /// <summary>
        /// Gets a value of the given type at the given key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetValue<T>(string key);

        /// <summary>
        /// Stores the object in Account Store at the given key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetValue(string key, object value);

        /// <summary>
        /// Removes the value at the given key from storage
        /// </summary>
        /// <param name="key"></param>
        void ClearValue(string key);
    }
}
