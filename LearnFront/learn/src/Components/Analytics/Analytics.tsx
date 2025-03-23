import './Analytics.css';
import React from 'react';
import  { ApexOptions } from 'apexcharts'
import ReactApexChart from 'react-apexcharts';

export default function Analytics() {
  const [state] = React.useState<{ series: ApexAxisChartSeries; options: ApexOptions }>({
    series: [
      {
        name: 'Words',
        data: [44, 55, 57, 56, 61, 58, 63, 60, 66]
      },
      {
        name: 'Sentences',
        data: [76, 85, 101, 98, 87, 105, 91, 114, 94]
      }
    ],
    options: {
      chart: {
        type: "bar",
        height: 350
      },
      plotOptions: {
        bar: {
          horizontal: false,
          columnWidth: '55%',
          borderRadius: 5,
          borderRadiusApplication: 'end'
        }
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        show: true,
        width: 2,
        colors: ['transparent']
      },
      xaxis: {
        categories: ['Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct']
      },
      yaxis: {
        title: {
          text: 'practiced count'
        }
      },
      fill: {
        opacity: 1
      },
      tooltip: {
        y: {
          formatter: function (val: number) {
            return "$ " + val;
          }
        }
      }
    }
  });
    return (
        <>
           <div className='analytics'> 
            <div id="chart">
                <ReactApexChart options={state.options} series={state.series} type="bar" height={350} />
              </div>
            <div id="html-dist"></div>
          </div>
        </>
    );
}