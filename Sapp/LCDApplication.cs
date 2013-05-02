using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogiFrame;
using System.Diagnostics;

namespace Sapp
{
    class LCDApplication
    {
        List<Tabs.Tab> tabs = new List<Tabs.Tab>();
        Frame lcd = new Frame(Sapp.Settings.Library == "G15" ? LogiFrame.Logitech.Keyboard.G15 : LogiFrame.Logitech.Keyboard.G510, "Sapp", 15);

        Tabs.Tab CurrentTab = null;
        public LCDApplication()
        {
            //Load all settings
            Settings.Load();

            lcd.SetUpdatePriority(UpdatePriority.Alert);

            //While offline
            tabs.Add(new Tabs.Offline(this));

            //While loading game
            tabs.Add(new Tabs.LoadingGame(this));

            //While in game
            tabs.Add(new Tabs.OnFoot(this));
            tabs.Add(new Tabs.InVehicle(this));
            tabs.Add(new Tabs.Messaging(this));
            //Always
            tabs.Add(new Tabs.Settings(this));

            ShowNextTab();

        }

        private Tabs.Tab GetNextTab(Type type)
        {
            Tabs.Tab firstTab = null;
            Tabs.Tab firstTabAfterMe = null;
            bool foundMe = false;
            foreach (Tabs.Tab t in tabs)
            {
                if (type != null && t.GetType() != type)
                    continue;
                if (!foundMe && firstTab == null && t.IsShowAble() && CurrentTab != t)
                    firstTab = t;

                if (foundMe && firstTabAfterMe == null && t.IsShowAble())
                    firstTabAfterMe = t;

                if (CurrentTab == t)
                    foundMe = true;
            }

            return firstTabAfterMe != null ? firstTabAfterMe : firstTab;
        }

        public void ShowNextTab()
        {
            ShowNextTab(null);
        }

        public void ShowNextTab(Type type)
        {
            Tabs.Tab nextTab = GetNextTab(type);

            if (nextTab == null)
                return;

            if (CurrentTab != null)
                CurrentTab.Hide(lcd);

            nextTab.Show(lcd);
            CurrentTab = nextTab;
        }
    }
}
