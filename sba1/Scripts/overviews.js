$(document).ready(function () {
    plotCpuOverview(50, 1000);
    plotRamOverview(50, 1000);
    plotDiskOverview(50, 1000);
    plotNicOverview(50, 1000);
});

function blanks(size) {
    var data = [];
    for (var i = 0; i < size; i++) {
        data[i] = [i, 0];
    }
    return data;
}

function addPoint(data, point) {
    var result = data.slice(1);
    result.push([result.length + 1, point]);
    for (var i = 0; i < result.length; i++) {
        result[i][0] -= 1;
    }
    return result;
}

function plotCpuOverview(plotSize, speed) {
    var container = $("#cpuPlotOverview");
    var cpuData = blanks(plotSize);

    var plot = $.plot(container, [cpuData], {
        grid: { borderWidth: 0 },
        series: {
            lines: {
                show: true,
                fill: true,
            },
            shadowSize: 0,
            color: 'white'
        },
        legend: { show: false },
        xaxis: {
            show: false
        },
        yaxis: {
            show: false,
            min: 0,
            max: 100
        }
    });

    var hub = $.connection.cpuOverviewHub;
    $.connection.hub.start().done(function () {
        hub.server.init(speed);
    });

    hub.client.report = function (cpuPoint) {
        $("#cpuOverview").text(Math.round(cpuPoint));
        cpuData = addPoint(cpuData, cpuPoint);
        plot.setData([cpuData]);
        plot.draw();
    }
}

function plotRamOverview(plotSize, speed) {
    var container = $("#ramPlotOverview");
    var ramData = blanks(plotSize);

    var plot = $.plot(container, [ramData], {
        grid: { borderWidth: 0 },
        series: {
            lines: {
                show: true,
                fill: true,
            },
            shadowSize: 0,
            color: 'white'
        },
        legend: { show: false },
        xaxis: {
            show: false
        },
        yaxis: {
            show: false,
            min: 0,
            max: 100
        }
    });

    var hub = $.connection.ramOverviewHub;
    $.connection.hub.start().done(function () {
        hub.server.init(speed);
    });

    hub.client.report = function (ramPoint) {
        $("#ramOverview").text(ramPoint);
        ramData = addPoint(ramData, ramPoint);
        plot.setData([ramData]);
        plot.draw();
    }
}

function plotDiskOverview(plotSize, speed) {
    var container = $("#diskPlotOverview");
    var ramData = blanks(plotSize);

    var plot = $.plot(container, [ramData], {
        grid: { borderWidth: 0 },
        series: {
            lines: {
                show: true,
                fill: true
            },
            shadowSize: 0,
            color: 'white'
        },
        legend: { show: false },
        xaxis: {
            show: false
        },
        yaxis: {
            show: false,
            min: 0,
            max: 100
        }
    });

    var hub = $.connection.diskOverviewHub;
    $.connection.hub.start().done(function () {
        hub.server.init(speed);
    });

    hub.client.report = function (ramPoint) {
        $("#diskOverview").text(ramPoint);
        ramData = addPoint(ramData, ramPoint);
        plot.setData([ramData]);
        plot.draw();
    }
}

function plotNicOverview(plotSize, speed) {
    var container = $("#nicPlotOverview");
    var nicData = blanks(plotSize);

    var plot = $.plot(container, [nicData], {
        grid: { borderWidth: 0 },
        series: {
            lines: {
                show: true,
                fill: true,
            },
            shadowSize: 0,
            color: 'white'
        },
        legend: { show: false },
        xaxis: {
            show: false
        },
        yaxis: {
            show: true,
            min: 0,
            tickColor: 'transparent',
            font: { size: 0 },
            tickFormatter: function (val, axis) {
                if (val > 1000000)
                    return (val / 1000000).toFixed(axis.tickDecimals) + " Mb";
                else if (val > 1000)
                    return (val / 1000).toFixed(axis.tickDecimals) + " Kb";
                else
                    return val.toFixed(axis.tickDecimals) + " b";
            }
        }
    });

    var hub = $.connection.nicOverviewHub;
    $.connection.hub.start().done(function () {
        hub.server.init(speed);
    });

    hub.client.report = function (nicPoint, max) {
        $("#nicOverview").text(Math.round((nicPoint * 100) / max));

        var ticks = plot.getAxes().yaxis.ticks;
        $("#maxY").text(ticks[ticks.length - 1].label);

        nicData = addPoint(nicData, nicPoint);
        plot.setData([nicData]);
        plot.setupGrid();
        plot.draw();
    }
}