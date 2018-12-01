using XamarinFirebaseMessaging.Services.Interfaces;
using Newtonsoft.Json;
using System.Linq;
using Xamarin.Auth;

namespace XamarinFirebaseMessaging.Services
{
    public class AccountStoreService : IAccountStoreService {
        private AccountStore _accountStore = null;
        private AccountStore AccountStore => _accountStore ?? (_accountStore = AccountStore.Create());

        private Account _account = null;
        private Account Account {
            get {
                _account = _account ?? (_account = AccountStore.FindAccountsForService(Constants.SERVICE_ID).FirstOrDefault());
                if (_account == null)
                {
                    _account = new Account();
                }
                return _account;
            }
            set {
                _account = value;
            }
        }

        public T GetValue<T>(string key)
        {
            if(Account.Properties.ContainsKey(key))
            {
                return JsonConvert.DeserializeObject<T>(Account.Properties[key]);
            } else
            {
                return default(T);
            }
        }

        public void SetValue(string key, object value)
        {
            Account.Properties[key] = JsonConvert.SerializeObject(value);
            SaveAccount();
        }

        public void ClearValue(string key)
        {
            if (Account.Properties.ContainsKey(key))
            {
                Account.Properties.Remove(key);
                SaveAccount();
            }
        }

        private void SaveAccount()
        {
            AccountStore.Save(_account, Constants.SERVICE_ID);
        }
    }
}
