using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Input;

namespace TodoAndris.Models
{
    public class Item : INotifyPropertyChanged
    {
        // These are the bits of information that we get from the server
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public int Days { get; set; }
        private int progress;
        public DateTime LastCompleted { get; set; }
        // End of the bits that are coming from the server

        public int Progress
        {
            get { return progress; }
            set
            {
                // Set progress seperately to avoid Stack Overflow
                progress = value;
                NotifyPropertyChanged("Progress");
                NotifyPropertyChanged("ProgressAmount");
            }
        }

        [IgnoreDataMember]
        // Calculates progress amount in a scale of 0..1
        public double ProgressAmount
        {
            get
            {
                // We are not animals, let's not divide by 0
                if (Days == 0)
                    return 0;
                return (double)Progress / Days;
            }
        }

        [IgnoreDataMember]
        // Command that gets executed when button "I did this today" is clicked
        public ICommand CompletedCommand { get; set; }

        [IgnoreDataMember]
        // Checks if the task was already completed today
        public bool IsEnabled {
            get
            {
                if(LastCompleted.Date == DateTime.Today)
                    return false;
                return true;
            }
            set
            {
                NotifyPropertyChanged("IsEnabled");
                NotifyPropertyChanged("IsCompleted");
            }
        }

        [IgnoreDataMember]
        // Checks if the task has been finished
        public bool IsCompleted
        {
            get
            {
                if (Progress >= Days)
                {
                    return true;
                }
                return false;
            }
        }

        public Item()
        {
            // Apply OnClick command on create
            CompletedCommand = new CompleteClicked();
        }

        // We want to tell the app that our properties have changed and they need to be updated in the interface
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}