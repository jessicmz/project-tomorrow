﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title></title>   
    <!-- Bootstrap -->
    <link href="speakup.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
</head>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/morris.js/0.5.1/morris.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"> 

<body>
    <form id="form1" runat="server">               
        <div class="wrapper">
            <div class="container">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-10">
                        <nav class="navbar navbar-default">
                            <div class="container-fluid">
                                <div class="navbar-header">
                                    <a class="navbar-brand" href="#">Project Tomorrow</a>
                                </div>
                                <ul class="nav navbar-nav pull-right">
                                    <li><a href="#">Home</a></li>
                                    <li><a href="#">Surveys</a></li>
                                    <li><a href="#">Logout</a></li>
                                </ul>
                            </div>
                        </nav>
                        <div class="page-header pageHeader">
                            <h1>
                                <asp:Label ID="surveyname" runat="server" Text="Label"></asp:Label>
                                <small>Dashboard</small></h1>
                            <i class="fa fa-download" style="font-size:28px"></i> 
                            <a href="DIST25669-55.xls">Download Survey Data</a>
                        </div>
                        <div class="contentBackground">
                            <div class="contents">
                                <div class="metricsWrapper">
                                    <div class="row">
                                        <div class="col-sm-3">

                                            <div class="panel panel-default">

                                                <div class="panel-body totalResponsesCss">

                                                    <p style="text-align:right;font-size:12px;" class="metricsLabel">Total Responses</p>

                                                    <i class="fa fa-folder-o" style="font-size:28px"></i>

                                                    <asp:Label ID="questionMetrics" Font-Bold ="true" Font-Size="26px" runat="server" Text="Label"></asp:Label>

                                                </div>

                                                <div class="panel-footer surveyLinkCss">

                                                    <a href="url">View Details</a>

                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-sm-3">

                                            <div class="panel panel-default">

                                                <div class="panel-body totalQuestionsCss">

                                                    <p style="text-align:right;font-size:12px;" class="metricsLabel"># of Questions</p>

                                                    <i class="fa fa-tag" style="font-size:28px"></i>

                                                    <asp:Label ID="Label1" Font-Bold ="true" Font-Size="26px" runat="server" Text="Label"></asp:Label>

                                                </div>

                                                <div class="panel-footer surveyLinkCss">

                                                    <a href="url">View Details</a>

                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-sm-3">

                                            <div class="panel panel-default">

                                                <div class="panel-body totalOpenQuestionsCss">

                                                    <p style="text-align:right;font-size:12px;" class="metricsLabel">Open Ended Questions</p>

                                                    <i class="fa fa-file" style="font-size:28px"></i>

                                                    <asp:Label ID="Label2" Font-Bold ="true" Font-Size="26px" runat="server" Text="Label"></asp:Label>

                                                </div>

                                                <div class="panel-footer surveyLinkCss">

                                                    <a href="url">View Details</a>

                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-sm-3">

                                            <div class="panel panel-default">

                                                <div class="panel-body totalMetricCss">

                                                    <p style="text-align:right;font-size:12px;" class="metricsLabel">Total Responses</p>

                                                    <i class="fa fa-check-square" style="font-size:28px"></i>

                                                    <asp:Label ID="Label3" Font-Bold ="true" Font-Size="26px" runat="server" Text="Label"></asp:Label>

                                                </div>

                                                <div class="panel-footer surveyLinkCss">

                                                    <a href="url">View Details</a>

                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="questionfilter">
                                    <div class="input-group">
                                        <div class="input-group-addon">Questions</div>
                                        <asp:DropDownList runat="server" ID="questionFilter" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <br>
                                <div class="graphWrapper">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <i class="fa fa-bar-chart-o fa-fw"></i> Project Tomorrow
                                                    <div class="pull-right">
                                                        <div class="btn-group">
                                                            <select id="projectTomorrowChartType">
                                                                <option value="0">Chart Type</option>  
                                                                <option value="line">Line</option>
                                                                <option value="column">Column</option>
                                                            </select>
                                                            <!--
                                                            <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                                                Graph Types
                                                                <span class="caret"></span>
                                                            </button>
                                                            <ul class="dropdown-menu pull-right" role="menu">
                                                                <li><a href="#">Line</a></li>
                                                                <li><a href="#">Bar</a></li>
                                                                <li><a href="#">Donut</a></li>
                                                            </ul>
                                                            -->
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div id="ProjectTomorrow" style="height: 300px"  class="ChartBox" ></div>
                                                    <!--
                                                    <asp:Chart ID="Chart1" runat="server">
                                                        <Series>
                                                            <asp:Series Name="Series1"></asp:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                    -->
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <i class="fa fa-bar-chart-o fa-fw"></i> National Percentages
                                                    <div class="pull-right">
                                                        <div class="btn-group">
                                                            <select id="nationalChartType">
                                                                <option value="0">Chart Type</option>  
                                                                <option value="line">Line</option>
                                                                <option value="column">Column</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div id="National" style="height: 300px"  class="ChartBox" ></div>
                                                    <!--
                                                    <asp:Chart ID="Chart2" runat="server">
                                                        <Series>
                                                            <asp:Series Name="Series1"></asp:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                    -->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <i class="fa fa-bar-chart-o fa-fw"></i> State Percentages
                                                    <div class="pull-right">
                                                        <div class="btn-group">
                                                            <select id="stateChartType">
                                                                <option value="0">Chart Type</option>  
                                                                <option value="line">Line</option>
                                                                <option value="column">Column</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div id="State" style="height: 300px"  class="ChartBox" ></div>
                                                    <!--
                                                    <asp:Chart ID="Chart3" runat="server">
                                                        <Series>
                                                            <asp:Series Name="Series1"></asp:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                    -->
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <i class="fa fa-bar-chart-o fa-fw"></i> Percentage Comparison
                                                    <div class="pull-right">
                                                        <div class="btn-group">
                                                            <select id="comparisonChartType">
                                                                <option value="0">Chart Type</option>  
                                                                <option value="line">Line</option>
                                                                <option value="column">Column</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div id="Comparison" style="height: 300px"  class="ChartBox" ></div>
                                                    <!--
                                                    <asp:Chart ID="Chart4" runat="server">
                                                        <Series>
                                                            <asp:Series Name="Series1"></asp:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                    -->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-1"></div>
            </div>
        </div>
    </form>

    <!-- Latest compiled and minified JavaScript -->
    <script src="Scripts/jquery-1.9.1.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/morris.js/0.5.1/morris.min.js"></script>
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="http://code.highcharts.com/modules/exporting.js"></script>

    <script>
        $(document).ready(function () {
            $(function () {
                var projecttomorrow = new Highcharts.Chart({
                    chart: {
                        renderTo: 'ProjectTomorrow',
                        type: 'column'
                    },
                    plotOptions: {
                        series: {
                            cursor: 'pointer'
                        },
                    },
                    title: {
                        text: '',
                    },
                    exporting: {
                        enabled: false
                    },
                    credits: {
                        enabled: false
                    },
                    subtitle: {
                        text: '',
                    },
                    xAxis: {
                        categories: ['Grade 3', 'Grade 4', 'Grade 5']
                    },
                    yAxis: {
                        min: 0,
                        max: 12,
                        tickInterval: 2,
                        title: {
                            text: 'Responses'
                        },
                        plotLines: [{
                            value: 0,
                            width: 1,
                            color: '#808080'
                        }]
                    },

                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: [{
                        showInLegend: false,
                        name: 'Project Tomorrow',
                        data: [1, 2, 3]
                    }]
                });

                var national = new Highcharts.Chart({
                    chart: {
                        renderTo: 'National',
                        type: 'column'
                    },
                    plotOptions: {
                        series: {
                            cursor: 'pointer'
                        },
                    },
                    title: {
                        text: '',
                    },
                    exporting: {
                        enabled: false
                    },
                    credits: {
                        enabled: false
                    },
                    subtitle: {
                        text: '',
                    },
                    xAxis: {
                        categories: ['Grade 3', 'Grade 4', 'Grade 5']
                    },
                    yAxis: {
                        min: 0,
                        max: 12,
                        tickInterval: 2,
                        title: {
                            text: 'Responses'
                        },
                        plotLines: [{
                            value: 0,
                            width: 1,
                            color: '#808080'
                        }]
                    },

                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: [{
                        showInLegend: false,
                        name: 'National',
                        data: [6, 3, 9]
                    }]
                });

                var state = new Highcharts.Chart({
                    chart: {
                        renderTo: 'State',
                        type: 'column'
                    },
                    plotOptions: {
                        series: {
                            cursor: 'pointer'
                        },
                    },
                    title: {
                        text: '',
                    },
                    exporting: {
                        enabled: false
                    },
                    credits: {
                        enabled: false
                    },
                    subtitle: {
                        text: '',
                    },
                    xAxis: {
                        categories: ['Grade 3', 'Grade 4', 'Grade 5']
                    },
                    yAxis: {
                        min: 0,
                        max: 12,
                        tickInterval: 2,
                        title: {
                            text: 'Responses'
                        },
                        plotLines: [{
                            value: 0,
                            width: 1,
                            color: '#808080'
                        }]
                    },

                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: [{
                        showInLegend: false,
                        name: 'State',
                        data: [10, 2, 3]
                    }]
                });

                var comparison = new Highcharts.Chart({
                    chart: {
                        renderTo: 'Comparison',
                        type: 'column'
                    },
                    plotOptions: {
                        series: {
                            cursor: 'pointer'
                        },
                    },
                    title: {
                        text: '',
                    },
                    exporting: {
                        enabled: false
                    },
                    credits: {
                        enabled: false
                    },
                    subtitle: {
                        text: '',
                    },
                    xAxis: {
                        categories: ['Grade 3', 'Grade 4', 'Grade 5']
                    },
                    yAxis: {
                        min: 0,
                        max: 12,
                        tickInterval: 2,
                        title: {
                            text: 'Responses'
                        },
                        plotLines: [{
                            value: 0,
                            width: 1,
                            color: '#808080'
                        }]
                    },

                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0,
                        labelFormatter: function () {
                            var legendName = this.name;
                            var match = legendName.match(/.{1,10}/g);
                            return match.toString().replace(/\,/g, "<br/>");
                        }
                    },
                    series: [{
                        name: 'Project Tomorrow',
                        data: [1, 2, 3]
                    }, {
                        name: 'National',
                        data: [6, 3, 9]
                    }, {
                        name: 'State',
                        data: [10, 2, 3]
                    }]
                });
            });

            $("#projectTomorrowChartType").change(function () {
                var type = this.value;
                if (type !== '0') {
                    var pt = $('#ProjectTomorrow').highcharts();
                    $(pt.series).each(function () {
                        this.update({
                            type: type
                        }, false);
                    });
                    pt.redraw();
                }
            });

            $("#nationalChartType").change(function () {
                var type = this.value;
                if (type !== '0') {
                    var nat = $('#National').highcharts();
                    $(nat.series).each(function () {
                        this.update({
                            type: type
                        }, false);
                    });
                    nat.redraw();
                }
            });

            $("#stateChartType").change(function () {
                var type = this.value;
                if (type !== '0') {
                    var sta = $('#State').highcharts();
                    $(sta.series).each(function () {
                        this.update({
                            type: type
                        }, false);
                    });
                    sta.redraw();
                }
            });

            $("#comparisonChartType").change(function () {
                var type = this.value;
                if (type !== '0') {
                    var com = $('#Comparison').highcharts();
                    $(com.series).each(function () {
                        this.update({
                            type: type
                        }, false);
                    });
                    com.redraw();
                }
            });
        });
    </script>

</body>
</html>
