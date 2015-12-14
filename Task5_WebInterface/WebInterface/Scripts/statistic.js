google.load("visualization", "1.0", { 'packages': ["corechart"] });

google.setOnLoadCallback(drawChart);

function drawChart() {

    $.post("/Sales/Statistic", {},
    function (data) {
        var tdata = new google.visualization.DataTable();

        tdata.addColumn("string", "Goods");
        tdata.addColumn("number", "Count");

        for (var i = 0; i < data.length; i++) {
            tdata.addRow([data[i].Name, data[i].Value]);
        }

        var options = {
            'title': "Top 5 goods by quantity of sales",
            'width': 800,
            'height': 600
        };

        var chart = new google.visualization.PieChart(document.getElementById("chart_div"));
        chart.draw(tdata, options);
    });
}