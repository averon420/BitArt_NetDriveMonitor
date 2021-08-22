﻿using ControlzEx.Theming;
using NetDriveManager.WPF.AppUI.Frame;
using NetDriveManager.WPF.AppUI.Home;
using NetDriveManager.WPF.utilities.contentController.services;
using Serilog;
using System.Windows.Controls;

namespace NetDriveManager.WPF.Main
{
	public class MainWindowModel : ViewModelBase
	{
		public ContentControl CCMain => _mainContent.Control;
		public ContentControl CCHeader { get; }
		public ContentControl CCFooter { get; }

		private readonly IContentControllerService _cc;
		private readonly MainContentStore _mainContent;

		public MainWindowModel(IContentControllerService contentControllerService, MainContentStore mainContentStore)
		{
			_cc = contentControllerService;

			_mainContent = mainContentStore;
			_mainContent.OnContentChanged += UpdateMainContent;
			_mainContent.Control = _cc.GetUserControl(nameof(HomeView));

			CCHeader = _cc.GetUserControl(nameof(HeaderView));
			CCFooter = _cc.GetUserControl(nameof(FooterView));
			SetTheme();
			Log.Debug("Main window loaded");
		}

		private void UpdateMainContent()
		{
			OnPropertyChanged(nameof(CCMain));
		}

		private void SetTheme()
		{
			ThemeManager.Current.ChangeTheme(App.Current, "Dark.Steel");
			ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncAll;
			Log.Debug("Theme set and sync enabled");
		}
	}
}