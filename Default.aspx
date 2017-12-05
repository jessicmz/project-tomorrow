<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

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
                                <ul class="nav navbar-nav">
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
                            <a href="#">Download Survey Data</a>
                        </div>
                        <div class="contentBackground">
                            <div class="contents">
                                <div class="metricsWrapper">
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="panel panel-default">
                                                <div class="panel-body totalResponsesCss">
                                                    <p class="metricsLabel">Total Responses</p>
                                                    <asp:Label ID="questionMetrics" CssClass="metricsCss" runat="server" Text="Label"></asp:Label>
                                                </div>
                                                <div class="panel-footer surveyLinkCss">
                                                    <a href="url">link text</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="panel panel-default">
                                                <div class="panel-body totalQuestionsCss">
                                                    <p class="metricsLabel"># of Questions</p>
                                                    <asp:Label ID="Label1" CssClass="metricsCss" runat="server" Text="Label"></asp:Label>
                                                </div>
                                                <div class="panel-footer surveyLinkCss">
                                                    <a href="url">link text</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="panel panel-default">
                                                <div class="panel-body totalOpenQuestionsCss">
                                                    <p class="metricsLabel"># of Open Ended Questions</p>
                                                    <asp:Label ID="Label2" CssClass="metricsCss" runat="server" Text="Label"></asp:Label>
                                                </div>
                                                <div class="panel-footer surveyLinkCss">
                                                    <a href="url">link text</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="panel panel-default">
                                                <div class="panel-body totalMetricCss">
                                                    <p class="metricsLabel">Total Responses</p>
                                                    <asp:Label ID="Label3" CssClass="metricsCss" runat="server" Text="Label"></asp:Label>
                                                </div>
                                                <div class="panel-footer surveyLinkCss">
                                                    <a href="url">link text</a>
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
                                                    <i class="fa fa-bar-chart-o fa-fw"></i> National
                                                    <div class="pull-right">
                                                        <div class="btn-group">
                                                            <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                                                Graph Types
                                                                <span class="caret"></span>
                                                            </button>
                                                            <ul class="dropdown-menu pull-right" role="menu">
                                                                <li><a href="#">Line</a>
                                                                </li>
                                                                <li><a href="#">Bar</a>
                                                                </li>
                                                                <li><a href="#">Donut</a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div id="national" style="height: 300px;"></div>
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
                                                    <i class="fa fa-bar-chart-o fa-fw"></i> State
                                                    <div class="pull-right">
                                                        <div class="btn-group">
                                                            <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                                                Graph Types
                                                                <span class="caret"></span>
                                                            </button>
                                                            <ul class="dropdown-menu pull-right" role="menu">
                                                                <li><a href="#">Line</a>
                                                                </li>
                                                                <li><a href="#">Bar</a>
                                                                </li>
                                                                <li><a href="#">Donut</a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div id="state" style="height: 300px;"></div>
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
                                                    <i class="fa fa-bar-chart-o fa-fw"></i> District
                                                    <div class="pull-right">
                                                        <div class="btn-group">
                                                            <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                                                Graph Types
                                                                <span class="caret"></span>
                                                            </button>
                                                            <ul class="dropdown-menu pull-right" role="menu">
                                                                <li><a href="#">Line</a>
                                                                </li>
                                                                <li><a href="#">Bar</a>
                                                                </li>
                                                                <li><a href="#">Donut</a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div id="district" style="height: 300px;"></div>
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
                                                    <i class="fa fa-bar-chart-o fa-fw"></i> Project Tomorrow
                                                    <div class="pull-right">
                                                        <div class="btn-group">
                                                            <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                                                Graph Types
                                                                <span class="caret"></span>
                                                            </button>
                                                            <ul class="dropdown-menu pull-right" role="menu">
                                                                <li><a href="#">Line</a>
                                                                </li>
                                                                <li><a href="#">Bar</a>
                                                                </li>
                                                                <li><a href="#">Donut</a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div id="projecttomorrow" style="height: 300px;"></div>
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

    <script>
        $('#demolist li').on('click', function () {
            $('#datebox').val($(this).text());
        });

        new Morris.Bar({
            // ID of the element in which to draw the chart.
            element: 'national',
            // Chart data records -- each entry in this array corresponds to a point on
            // the chart.
            data: [
                { year: '2008', value: 20 },
                { year: '2009', value: 10 },
                { year: '2010', value: 5 },
                { year: '2011', value: 5 },
                { year: '2012', value: 20 }
            ],
            // The name of the data record attribute that contains x-values.
            xkey: 'year',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['value'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Value']
        });

        new Morris.Bar({
            // ID of the element in which to draw the chart.
            element: 'state',
            // Chart data records -- each entry in this array corresponds to a point on
            // the chart.
            data: [
                { year: '2008', value: 20 },
                { year: '2009', value: 10 },
                { year: '2010', value: 5 },
                { year: '2011', value: 5 },
                { year: '2012', value: 20 }
            ],
            // The name of the data record attribute that contains x-values.
            xkey: 'year',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['value'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Value']
        });

        new Morris.Bar({
            // ID of the element in which to draw the chart.
            element: 'district',
            // Chart data records -- each entry in this array corresponds to a point on
            // the chart.
            data: [
                { year: '2008', value: 20 },
                { year: '2009', value: 10 },
                { year: '2010', value: 5 },
                { year: '2011', value: 5 },
                { year: '2012', value: 20 }
            ],
            // The name of the data record attribute that contains x-values.
            xkey: 'year',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['value'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Value']
        });

        new Morris.Bar({
            // ID of the element in which to draw the chart.
            element: 'projecttomorrow',
            // Chart data records -- each entry in this array corresponds to a point on
            // the chart.
            data: [
                { year: '2008', value: 20 },
                { year: '2009', value: 10 },
                { year: '2010', value: 5 },
                { year: '2011', value: 5 },
                { year: '2012', value: 20 }
            ],
            // The name of the data record attribute that contains x-values.
            xkey: 'year',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['value'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Value']
        });
    </script>

</body>
</html>
