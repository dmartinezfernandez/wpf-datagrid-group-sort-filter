using System.ComponentModel;

namespace WpfDatagridGrouping;

// Task Class
// Requires using System.ComponentModel;
public class Task : INotifyPropertyChanged, IEditableObject
{
    // The Task class implements INotifyPropertyChanged and IEditableObject
    // so that the datagrid can properly respond to changes to the
    // data collection and edits made in the DataGrid.

    // Private task data.
    private string m_ProjectName = string.Empty;
    private string m_TaskName = string.Empty;
    private DateTime m_DueDate = DateTime.Now;
    private bool m_Complete = false;

    // Data for undoing canceled edits.
    private Task temp_Task = null;
    private bool m_Editing = false;

    // Public properties.
    public string ProjectName
    {
        get { return this.m_ProjectName; }
        set
        {
            if (value != this.m_ProjectName)
            {
                this.m_ProjectName = value;
                NotifyPropertyChanged("ProjectName");
            }
        }
    }

    public string TaskName
    {
        get { return this.m_TaskName; }
        set
        {
            if (value != this.m_TaskName)
            {
                this.m_TaskName = value;
                NotifyPropertyChanged("TaskName");
            }
        }
    }

    public DateTime DueDate
    {
        get { return this.m_DueDate; }
        set
        {
            if (value != this.m_DueDate)
            {
                this.m_DueDate = value;
                NotifyPropertyChanged("DueDate");
            }
        }
    }

    public bool Complete
    {
        get { return this.m_Complete; }
        set
        {
            if (value != this.m_Complete)
            {
                this.m_Complete = value;
                NotifyPropertyChanged("Complete");
            }
        }
    }

    // Implement INotifyPropertyChanged interface.
    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Implement IEditableObject interface.
    public void BeginEdit()
    {
        if (m_Editing == false)
        {
            temp_Task = this.MemberwiseClone() as Task;
            m_Editing = true;
        }
    }

    public void CancelEdit()
    {
        if (m_Editing == true)
        {
            this.ProjectName = temp_Task.ProjectName;
            this.TaskName = temp_Task.TaskName;
            this.DueDate = temp_Task.DueDate;
            this.Complete = temp_Task.Complete;
            m_Editing = false;
        }
    }

    public void EndEdit()
    {
        if (m_Editing == true)
        {
            temp_Task = null;
            m_Editing = false;
        }
    }
}