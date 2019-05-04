var MONTHS = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

var config = {
	type: 'line',
	data: {
		labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
		datasets: [{
			label: 'Trainings',
			//fill: true,
			borderColor: window.chartColors.red,
			backgroundColor: window.chartColors.red_t,
			data: [
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor()
			],
		}, {
			label: 'Sessions',
			//fill: false,
			borderColor: window.chartColors.blue,
			backgroundColor: window.chartColors.blue_t,
			data: [
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor()
			],
		}, {
			label: 'Roster',
			fill: false,
			borderColor: window.chartColors.green,
			backgroundColor: '#fff',
			data: [
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor(),
				randomScalingFactor(),
				10
			],
		}]
	},
	options: {
		responsive: true,
		title: {
			display: false,
			text: 'Chart.js Line Chart - Stacked Area'
		},
		tooltips: {
			mode: 'index',
		},
		hover: {
			mode: 'index'
		},
		scales: {
			xAxes: [{
				//scaleLabel: {
				//	display: true,
				//	labelString: 'Month'
				//}
			}],
			yAxes: [{
				//stacked: true,
				//scaleLabel: {
				//	display: true,
				//	labelString: 'Value'
				//}
			}]
		}
	}
};

window.onload = function () {
	var ctx = document.getElementById('canvas').getContext('2d');
	window.myLine = new Chart(ctx, config);
};

//============================
var randomScalingFactor = function () {
	return Math.round(Math.random() * 100);
};

var configPie1 = {
	type: 'pie',
	data: {
		datasets: [{
			data: [
				randomScalingFactor(),
				randomScalingFactor(),
			],
			backgroundColor: [
				window.chartColors.blue,
				window.chartColors.red_t
			],
			label: 'Dataset 1'
		}],
		labels: [
			'Active Trainings',
			'Inactive Trainings'
		]
	},
	options: {
		responsive: false
	}
};

var configPie2 = {
	type: 'pie',
	data: {
		datasets: [{
			data: [
				randomScalingFactor(),
				randomScalingFactor(),
			],
			backgroundColor: [
				window.chartColors.blue,
				window.chartColors.red_t
			],
			label: 'Dataset 1'
		}],
		labels: [
			'Active Sessions',
			'Inactive Sessions'
		]
	},
	options: {
		responsive: false
	}
};

$(function () {
	var ctx1 = document.getElementById('chart-pie1').getContext('2d');
	var ctx2 = document.getElementById('chart-pie2').getContext('2d');

	window.myPie1 = new Chart(ctx1, configPie1);
	window.myPie2 = new Chart(ctx2, configPie2);
});

