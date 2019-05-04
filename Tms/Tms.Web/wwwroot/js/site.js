$(function () {
	"use strict";

	//Object atc
	//==================================
	window.atc = {
		currentPage: null,

		closeAddEditPage: function (objPage) {
			if (atc.currentPage) atc.currentPage.show();
			else if (objPage) objPage.show();
			else atc.home.show();
		},
		showHideHeadBarButtons: function(){
			$("#newTrainingButton, #newSessionButton, #addRosterFromPopulationButton").hide();
			switch (atc.currentPage) {
				case atc.sessionsCal:
					$("#newSessionButton").show();
					break;
				case atc.sessionsList:
					$("#newSessionButton").show();
					break;
				case atc.roster:
					$("#addRosterFromPopulationButton").show();
					break;
				default:
					$("#newTrainingButton").show();
			}
		},
		hideAll: function (isInitial) {
			if (isInitial) {
				this.trainings.addContainer.hide();
			}
			else {
				this.dash.hide();
				this.trainings.hide();
			}

			this.sessionsCal.hide();
			this.sessionsList.hide();
			this.roster.hide();
		},
		setPageTitle: function (titleHtml) {
			$("#pageTitle").html(titleHtml);
		},

		home: {
			show: function () {
				atc.hideAll();
				atc.trainings.show();
				atc.dash.mainContainer.show();

				atc.currentPage = this;
			}
		},
		dash: {
			mainContainer: $("#dashContainer"),
			hide: function () {
				this.mainContainer.hide();
			},
			show: function () {
				atc.setPageTitle("Dashboard");
				atc.hideAll();
				this.mainContainer.show();

				atc.currentPage = this;
				atc.showHideHeadBarButtons();
			}
		},
		trainings: {
			mainContainer: $("#trainingsContainer"),
			addContainer: $("#trainingAddContainer"),
			hide: function () {
				this.mainContainer.hide();
				this.addContainer.hide();
				if (atc.trainingsTable) atc.trainingsTable.fixedHeader.disable();
			},
			show: function () {
				atc.setPageTitle("List of Trainings");
				atc.hideAll();
				this.mainContainer.show();
				atc.trainingsTable.fixedHeader.enable();
				atc.trainingsTable.fixedHeader.adjust();

				atc.currentPage = this;
				atc.showHideHeadBarButtons();

			},
			showAdd: function () {
				atc.setPageTitle("Create New Training");
				atc.hideAll();
				this.addContainer.show();

			},
			showEdit: function () {
				atc.setPageTitle("Edit Training");
				atc.hideAll();
				this.addContainer.show();
			}
		},
		sessionsCal: {
			mainContainer: $("#sessionsContainerCal"),
			hide: function () {
				this.mainContainer.hide();
			},
			show: function () {
				atc.setPageTitle("Sessions Calendar");
				atc.hideAll();
				this.mainContainer.show();

				atc.currentPage = this;
				atc.showHideHeadBarButtons();
			}
		},
		sessionsList: {
			mainContainer: $("#sessionsContainerList"),
			addContainer: $("#sessionAddContainer"),
			hide: function () {
				this.mainContainer.hide();
				this.addContainer.hide();
				if (atc.sessionsTable) atc.sessionsTable.fixedHeader.disable();
			},
			show: function () {
				atc.setPageTitle("List of Sessions");
				atc.hideAll();
				this.mainContainer.show();
				atc.sessionsTable.fixedHeader.enable();
				atc.sessionsTable.fixedHeader.adjust();

				atc.currentPage = this;
				atc.showHideHeadBarButtons();
			},
			showAdd: function () {
				atc.setPageTitle("Create New Session");
				atc.hideAll();
				this.addContainer.show();

			},
			showEdit: function () {
				atc.setPageTitle("Edit Session");
				atc.hideAll();
				this.addContainer.show();
			}
		},
		roster: {
			mainContainer: $("#rosterContainer"),
			hide: function () {
				this.mainContainer.hide();
				if (atc.rosterTable) atc.rosterTable.fixedHeader.disable();
			},
			show: function () {
				atc.setPageTitle("Roster");
				atc.hideAll();
				this.mainContainer.show();
				atc.rosterTable.fixedHeader.enable();
				atc.rosterTable.fixedHeader.adjust();

				atc.currentPage = this;
				atc.showHideHeadBarButtons();
			}
		}
	};

	//Main menu navigation
	//==================================
	{
		$("#mainMenu a").click(function () {
			$("#mainMenuButton").dropdown("toggle");
		});

		$("#mainMenuDash").click(function () {
			atc.dash.show();
			return false;
		});

		$("#mainMenuTranings").click(function () {
			atc.trainings.show();
			return false;
		});

		$("#mainMenuSessionsCal").click(function () {
			atc.sessionsCal.show();
			return false;
		});

		$("#mainMenuSessionsList").click(function () {
			atc.sessionsList.show();
			return false;
		});

		$("#mainMenuRoster").click(function () {
			atc.roster.show();
			return false;
		});
	}
});