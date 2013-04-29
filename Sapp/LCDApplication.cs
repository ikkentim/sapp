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
            tabs.Add(new Tabs.Offline(this));
            tabs.Add(new Tabs.LoadingGame(this));
            tabs.Add(new Tabs.OnFoot(this));

            ShowNextTab();
        }

        public void ShowNextTab()
        {
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
        }
    }
}
