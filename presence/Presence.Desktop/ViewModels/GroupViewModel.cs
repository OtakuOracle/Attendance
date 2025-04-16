using Avalonia.Controls.Selection;
using domain.UseCase;
using DynamicData;
using DynamicData.Binding;
using presence.data.RemoteData.RemoteDataBase;
using presence.data.Repository;
using presence.domain.UseCase;
using Presence.Desktop.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Tmds.DBus.Protocol;

namespace Presence.Desktop.ViewModels
{
    public class GroupViewModel : ViewModelBase, IRoutableViewModel
    {
        public ICommand OpenFileDialog { get; }
        public Interaction<string?, string?> SelectFileInteraction => _SelectFileInteraction;
        public readonly Interaction<string?, string?> _SelectFileInteraction;
        private string? _selectedFile;
        public string? SelectedFile
        {
            get => _selectedFile;
            set => this.RaiseAndSetIfChanged(ref _selectedFile, value);
        }

        private readonly List<GroupPresenter> _groupPresentersDataSource = new List<GroupPresenter>();
        private ObservableCollection<GroupPresenter> _groups;
        public ObservableCollection<GroupPresenter> Groups => _groups;

        public GroupPresenter? SelectedGroupItem
        {
            get => _selectedGroupItem;
            set => this.RaiseAndSetIfChanged(ref _selectedGroupItem, value);
        }

        private GroupPresenter? _selectedGroupItem;



        private IGroupUseCase _groupUseCase;
        public ObservableCollection<UserPresenter> Users { get => _users; }
        public ObservableCollection<UserPresenter> _users;
        public GroupViewModel(IGroupUseCase groupUseCase)
        {
            foreach (var item in groupUseCase.GetGroupsWithStudents())
            {
                GroupPresenter groupPresenter = new GroupPresenter
                {
                    Id = item.Id,
                    Name = item.Name,
                    Users = item.User?.Select(user => new UserPresenter
                    {
                        Name = user.FIO,
                        Id = user.Id,
                        Group = new GroupPresenter { Id = item.Id, Name = item.Name }
                    }
                    ).ToList()
                };
                _groupPresentersDataSource.Add(groupPresenter);
            }
            _groups = new ObservableCollection<GroupPresenter>(_groupPresentersDataSource);

            _groupUseCase = groupUseCase;
            _SelectFileInteraction = new Interaction<string?, string?>();
            _users = new ObservableCollection<UserPresenter>();

            RefreshGroups();
            this.WhenAnyValue(vm => vm.SelectedGroupItem)
                .Subscribe(_ =>
                {
                    RefreshGroups();
                    SetUsers();
                });
            RemoveUserCommand = ReactiveCommand.Create<UserPresenter>(RemoveUser);
            RemoveAllSelectedCommand = ReactiveCommand.Create(RemoveAllSelected);
        }

        private void SetUsers()
        {
            if (SelectedGroupItem == null) return;
            if (SelectedGroupItem.Users == null) return;
            Users.Clear();
            GroupPresenter group = _groups.First(it => it.Id == SelectedGroupItem.Id);
            if (group.Users == null) return;
            foreach (var item in group.Users)
            {
                Users.Add(item);
            }
        }



        private void RefreshGroups()
        {
            _groupPresentersDataSource.Clear();
            foreach (var item in _groupUseCase.GetGroupsWithStudents())
            {
                GroupPresenter groupPresenter = new GroupPresenter
                {
                    Id = item.Id,
                    Name = item.Name,
                    Users = item.User?.Select(user => new UserPresenter
                    {
                        Name = user.FIO,
                        Id = user.Id,
                        Group = new GroupPresenter { Id = item.Id, Name = item.Name }
                    }
                    ).ToList()
                };
                _groupPresentersDataSource.Add(groupPresenter);
            }
            _groups = new ObservableCollection<GroupPresenter>(_groupPresentersDataSource);
        }

        private bool _MultipleSelected = false;
        public bool MultipleSelected { get => _MultipleSelected; set => this.RaiseAndSetIfChanged(ref _MultipleSelected, value); }

        public SelectionModel<UserPresenter> Selection { get; }
        void SelectionChanged(object sender, SelectionModelSelectionChangedEventArgs e)
        {
            MultipleSelected = Selection.SelectedItems.Count > 1;
        }

        private void RemoveUser(UserPresenter user)
        {
            if (user == null || SelectedGroupItem == null) return;

            _groupUseCase.RemoveUserFromGroup(user.Id);
            RefreshGroups();
            SetUsers();
        }


        private void RemoveAllSelected()
        {
            if (SelectedGroupItem == null) return;

            var selectedUsers = Selection.SelectedItems.ToList();
            foreach (var user in selectedUsers)
            {
                _groupUseCase.RemoveUserFromGroup(user.Id);
            }
            RefreshGroups();
            SetUsers();
        }

        public ICommand RemoveUserCommand { get; }
        public ICommand RemoveAllSelectedCommand { get; }

        
        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }
    }
}