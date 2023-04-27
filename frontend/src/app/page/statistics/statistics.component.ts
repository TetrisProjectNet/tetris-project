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
    borderWidth: 0,
    borderColor: '#212121',
    hoverOffset: 10,
    borderRadius: 0,
  } ];
  pieChartLegend = true;
  pieChartPlugins = [];
  // borderWidth = 0;

  // Radar
  radarChartOptions: ChartOptions<'radar'> = {
    responsive: true,
  };
  radarChartLabels: any = [];
  radarChartDatasets: any = [ {
    data: [],
    backgroundColor: [],
    borderWidth: 0,
    borderColor: '#212121',
    hoverOffset: 10,
    borderRadius: 0,
  } ];
  radarChartLegend = true;
  radarChartPlugins = [];
  // borderWidth = 0;

  

  faStar = faStar;

  constructor(
    private userService: UserService,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    AOS.init({
      duration: 600,
      offset: 130,
      easing: 'ease-in-sine'
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
    let lastGameCount = 7;
    this.list$.subscribe((allUsers) => {
      this.loggedUser$.subscribe((loggedUser) => {

        this.pieChartLabels = [];

      })
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
