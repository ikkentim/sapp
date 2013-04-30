using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogiFrame;

namespace Sapp
{
    class LCDApplication
    {
        List<Tabs.Tab> tabs = new List<Tabs.Tab>();
        Frame lcd = new Frame(LogiFrame.Logitech.Keyboard.G510, "Sapp", 15);

        Tabs.Tab CurrentTab = null;
        public LCDApplication()
        {
            lcd.SetUpdatePriority(UpdatePriority.Alert);

            //While offline
            tabs.Add(new Tabs.Offline(this));

            //While loading game
            tabs.Add(new Tabs.LoadingGame(this));

            //While in game
            tabs.Add(new Tabs.OnFoot(this));
            tabs.Add(new Tabs.InVehicle(this));
            ShowNextTab();
        }

        private Tabs.Tab GetNextTab()
        {
            Tabs.Tab firstTab = null;
            Tabs.Tab firstTabAfterMe = null;
            bool foundMe = false;
            foreach (Tabs.Tab t in tabs)
            {
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
            Tabs.Tab nextTab = GetNextTab();

            if (nextTab == null)
                return;

            if(CurrentTab != null)
                CurrentTab.Hide(lcd);

            nextTab.Show(lcd);
            CurrentTab = nextTab;

            /*
            if (CurrentTab == null)
            {
                foreach (Tabs.Tab t in tabs)
                {
                    if (t.IsShowAble())
                    {
                        CurrentTab = t;
                        t.Show(lcd);
                    }
                }
            }
            else
            {
                bool foundCurrent = false;
                Tabs.Tab firstAvailable = null;
                foreach (Tabs.Tab t in tabs)
                {
                    if (t == CurrentTab)
                    {
                        foundCurrent=true;
                        continue;
                    }
                    bool s = t.IsShowAble();

                    if (!foundCurrent && firstAvailable == null && s)
                        firstAvailable = t;

                    if (foundCurrent && s)
                    {
                        CurrentTab = t;
                        t.Show(lcd);
                        return;
                    }
                }
                if (firstAvailable != null)
                {
                    CurrentTab = firstAvailable;
                    firstAvailable.Show(lcd);
                }
            }
            */
        }
    }
}
