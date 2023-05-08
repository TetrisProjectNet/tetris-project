using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Controls {
    public class AuthEntry : Entry {
        public AuthEntry() {
            TextChanged += AuthEntry_TextChanged;
        }

        private void AuthEntry_TextChanged(object sender, TextChangedEventArgs e) {
            if (e.OldTextValue != null && e.NewTextValue.Length != e.OldTextValue.Length) {
                Image usernameFail = (Image)FindByName("usernameFail");
                Image passwordFail = (Image)FindByName("passwordFail");
                usernameFail.IsVisible = false;
                passwordFail.IsVisible = false;
            }
        }
    }
}
