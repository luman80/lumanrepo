$(document).ready(function () {
    var container = $("#cpuPlotOverview");
    var plotSize = 50;

    function blanks() {
        var data = [];
        for (var i = 0; i < plotSize; i++) {
            data[i] = [i, 0];
        }
        return data;
    }
    var cpuData = blanks();

    function addPoint(data, point) {
        var result = data.slice(1);
        result.push([result.length + 1, point]);
        for (var i = 0; i < result.length; i++) {
            result[i][0] -= 1;
        }
        return result;
    }

    var plot = $.plot(container, [cpuData], {
        grid: { borderWidth: 0 },
        series: {
            lines: {
                show: true,
                fill: true,
            },
            shadowSize: 0
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

    var hub = $.connection.overviewHub;
    $.connection.hub.start().done(function () {
        hub.server.init(1000);
    });

    hub.client.report = function (cpuPoint) {
        $("#cpuOverview").text(Math.round(cpuPoint));

        cpuData = addPoint(cpuData, cpuPoint);
        plot.setData([cpuData]);
        plot.draw();
    }
})