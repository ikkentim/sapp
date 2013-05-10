using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sapp
{
    public class MessageSet
    {
        private string _name="";

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private string _messageOneName="";
        public string MessageOneName
        {
            get
            {
                return _messageOneName;
            }
            set
            {
                _messageOneName = value;
            }
        }

        private string _messageOneContent="";
        public string MessageOneContent
        {
            get
            {
                return _messageOneContent;
            }
            set
            {
                _messageOneContent = value;
            }
        }

        private string _messageTwoName="";
        public string MessageTwoName
        {
            get
            {
                return _messageTwoName;
            }
            set
            {
                _messageTwoName = value;
            }
        }

        private string _messageTwoContent="";
        public string MessageTwoContent
        {
            get
            {
                return _messageTwoContent;
            }
            set
            {
                _messageTwoContent = value;
            }
        }

        public MessageSet(string Name, string MessageOneName, string MessageOneContent, string MessageTwoName, string MessageTwoContent)
        {
            this.Name = Name;
            this.MessageOneName = MessageOneName;
            this.MessageOneContent = MessageOneContent;
            this.MessageTwoName = MessageTwoName;
            this.MessageTwoContent = MessageTwoContent;
        }

        public MessageSet()
        {
        }

        public override string ToString()
        {
            return this.Name + ": " + this.MessageOneName + "=" + this.MessageOneContent + ", " + this.MessageTwoName + "=" + this.MessageTwoContent;
        }
    }
}
