using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfDatagridGrouping;

public partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel()
    {
        // Generate some task data and add it to the task list.
        for (var i = 1; i <= 14; i++)
        {
            Tasks.Add(new Task()
            {
                ProjectName = "Project " + ((i % 3) + 1),
                TaskName = "Task " + i,
                DueDate = DateTime.Now.AddDays(i),
                Complete = (i % 2 == 0)
            });
        }

        var tasks = TasksCollectionView =
            CollectionViewSource.GetDefaultView(Tasks);

        // group items
        if (tasks != null && tasks.CanGroup == true)
        {
            tasks.GroupDescriptions.Clear();
            tasks.GroupDescriptions.Add(
                new PropertyGroupDescription("ProjectName"));
            tasks.GroupDescriptions.Add(
                new PropertyGroupDescription("Complete"));
        }

        // sort items
        if (tasks != null && tasks.CanSort == true)
        {
            tasks.SortDescriptions.Clear();
            tasks.SortDescriptions.Add(new SortDescription("ProjectName",
                ListSortDirection.Ascending));
            tasks.SortDescriptions.Add(new SortDescription("Complete",
                ListSortDirection.Ascending));
            tasks.SortDescriptions.Add(
                new SortDescription("DueDate", ListSortDirection.Ascending));
        }

        // filtering
        if (tasks is { CanGroup: true })
            tasks.Filter = TaskFilter;
    }

    private bool TaskFilter(object item) =>
        !(IsChecked && item as Task is { Complete: true });

    [ObservableProperty] private ICollectionView? _tasksCollectionView;
    [ObservableProperty] private ObservableCollection<Task> _tasks = new();

    [ObservableProperty] private bool _isChecked;

    [RelayCommand]
    private void ToggleCheckBox() => TasksCollectionView?.Refresh();

    [RelayCommand]
    private void Ungroup() => TasksCollectionView?.GroupDescriptions.Clear();

    [RelayCommand]
    private void Group()
    {
        ICollectionView? tasks = TasksCollectionView;
        if (tasks is { CanGroup: true })
        {
            tasks.GroupDescriptions.Clear();
            tasks.GroupDescriptions.Add(
                new PropertyGroupDescription("ProjectName"));
            tasks.GroupDescriptions.Add(
                new PropertyGroupDescription("Complete"));
        }
    }
}