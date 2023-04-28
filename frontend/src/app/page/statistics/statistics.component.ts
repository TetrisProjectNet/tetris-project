import { Component, OnInit } from '@angular/core';
import { faStar } from '@fortawesome/free-solid-svg-icons';
import { UserService } from 'src/app/service/user.service';
import * as AOS from 'aos';
import { ChartOptions } from 'chart.js/dist/types/index';
import { ChartConfiguration, ChartDataset } from 'chart.js';
import { ShopItem } from 'src/app/model/shop-item';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.scss']
})
export class StatisticsComponent implements OnInit {

  list$ = this.userService.getAll();
  chartData: ChartDataset[] = [];
  loggedUser$ = this.authService.loggedUser$;

  loaded: boolean = false;
  // Pie
  pieChartOptions: ChartOptions<'pie'> = {
    responsive: true,
  };
  pieChartLabels: any = [];
  pieChartDatasets: any = [ {
    data: [],
    backgroundColor: [],
    borderWidth: 3,
    borderColor: '#212121',
    offset: 0,
    hoverOffset: 10,
    borderRadius: 0,
  } ];
  pieChartLegend = false;
  pieChartPlugins = [];
  // borderWidth = 0;

  // Radar
  radarChartOptions: ChartOptions<'radar'> = {
    responsive: true,
    plugins: {
      legend: {
        labels: {
          color: '#999'
        }
      }
    },
    scales: {
      r: {
        ticks: {
          color: 'rgba(0, 255, 255, 0.2)',
          backdropColor: '#212121'
        },
        grid: {
          color: 'rgba(0, 255, 255, 0.1)'
        },
        pointLabels: {
          color: '#999'
        }
      }
    }
  };
  radarChartLabels: any = [];
  radarChartDatasets: any = [  ];
  radarChartLegend = true;
  radarChartPlugins = [];

  radarData: any;


  faStar = faStar;

  constructor(
    private userService: UserService,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    AOS.init({
      duration: 600,
      offset: 130,
      easing: 'ease-in-sine',
      anchorPlacement: 'bottom-bottom',
    });

    this.getPieChartConfig();
    this.getRadarChartConfig();
  }

  getPieChartConfig() {
    this.list$.subscribe((data) => {
      // data = data.slice(0,5);
      let sortedData: any = {
        Itemless: [0, 'rgba(0, 0, 0, 0.3)']
      };

      data.map(user => {
        if (user.shopItems && user.shopItems.length != 0) {
          user.shopItems.map(shopItem => {
            if (!sortedData.hasOwnProperty(shopItem.title)) {
              sortedData[`${shopItem.title}`] = [1, shopItem.color];
            } else {
              sortedData[`${shopItem.title}`][0]++;
            }
          })
        } else {
          sortedData.Itemless[0]++;
        }
      })

      for (const key in sortedData) {
        this.pieChartLabels = [...this.pieChartLabels, key];
        this.pieChartDatasets[0].data = [...this.pieChartDatasets[0].data, sortedData[key][0]];
        this.pieChartDatasets[0].backgroundColor = [...this.pieChartDatasets[0].backgroundColor, sortedData[key][1]];
      }
      this.loaded = true;

    });
  }

  getRadarChartConfig() {
    // let data = {
    //   labels: [
    //     'Eating',
    //     'Drinking',
    //     'Sleeping',
    //     'Designing',
    //     'Coding',
    //     'Cycling',
    //     'Running'
    //   ],
    //   datasets: [{
    //     label: 'My First Dataset',
    //     data: [65, 59, 90, 81, 56, 55, 40],
    //     fill: true,
    //     backgroundColor: 'rgba(255, 99, 132, 0.2)',
    //     borderColor: 'rgb(255, 99, 132)',
    //     pointBackgroundColor: 'rgb(255, 99, 132)',
    //     pointBorderColor: '#fff',
    //     pointHoverBackgroundColor: '#fff',
    //     pointHoverBorderColor: 'rgb(255, 99, 132)'
    //   }, {
    //     label: 'My Second Dataset',
    //     data: [28, 48, 40, 19, 96, 27, 100],
    //     fill: true,
    //     backgroundColor: 'rgba(54, 162, 235, 0.2)',
    //     borderColor: 'rgb(54, 162, 235)',
    //     pointBackgroundColor: 'rgb(54, 162, 235)',
    //     pointBorderColor: '#fff',
    //     pointHoverBackgroundColor: '#fff',
    //     pointHoverBorderColor: 'rgb(54, 162, 235)'
    //   }]
    // };

    let lastGameCount = 7;
    this.loggedUser$.subscribe((loggedUser) => {
      if (loggedUser && loggedUser.scores && loggedUser.scores.length >= 7) {
        this.list$.subscribe((allUsers) => {
          let avgScores: number[] = [0, 0, 0, 0, 0, 0, 0];
          let divider = 0;
          allUsers.map(oneUser => {
            if (oneUser.scores && oneUser?.scores?.length >= 7) {
              let j = 0;
              for (let i = oneUser.scores.length - 1; i > oneUser.scores.length - 8; i--) {
                avgScores[j] += oneUser.scores[i];
                j++;
              }
              divider++;
            }
          })

          let avgUserData = {
            data: [0, 0, 0, 0, 0, 0, 0],
            label: '',
            backgroundColor: '',
            hoverBackgroundColor: '',
            borderColor: '',
            labelColor: '#999',
            borderWidth: 2,
            fill: true,
            order: 2
          }
          avgScores = avgScores.map(avgScore => Math.round(avgScore / divider));

          avgUserData.data = avgScores;
          avgUserData.backgroundColor = 'rgba(255, 99, 132, 0.2)';
          avgUserData.hoverBackgroundColor = 'rgba(255, 99, 132, 0.4)';
          avgUserData.borderColor = 'rgb(255, 99, 132)';
          avgUserData.label = 'Avg Player';

          this.radarChartDatasets.push(avgUserData);
          this.radarChartLabels = ['Latest', '2nd', '3rd', '4th', '5th', '6th', '7th'];

          let loggedUserData = {
            data: loggedUser.scores,
            label: `${loggedUser.username}`,
            backgroundColor: 'rgba(54, 162, 235, 0.2)',
            hoverBackgroundColor: 'rgba(54, 162, 235, 0.4)',
            borderColor: 'rgb(54, 162, 235)',
            borderWidth: 2,
            fill: true,
            order: 1
          }
          this.radarChartDatasets.push(loggedUserData);

          // loggedUser.scores?.map(score => {
          // })

        })

      }
    })
  }


  // public radarChartOptions: ChartConfiguration<'radar'>['options'] = {
  //   responsive: true,
  // };
  // public radarChartLabels: string[] = ['Eating', 'Drinking', 'Sleeping', 'Designing', 'Coding', 'Cycling', 'Running'];

  // public radarChartDatasets: ChartConfiguration<'radar'>['data']['datasets'] = [
  //   { data: [65, 59, 90, 81, 56, 55, 40], label: 'Series A' },
  //   { data: [28, 48, 40, 19, 96, 27, 100], label: 'Series B' }
  // ];


}
