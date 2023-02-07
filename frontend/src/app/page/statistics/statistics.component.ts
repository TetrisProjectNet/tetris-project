import { Component, OnInit } from '@angular/core';
import { faStar } from '@fortawesome/free-solid-svg-icons';
import { UserService } from 'src/app/service/user.service';
import * as AOS from 'aos';
import { ChartOptions } from 'chart.js/dist/types/index';
import { ChartDataset } from 'chart.js';
import { ShopItem } from 'src/app/model/shop-item';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.scss']
})
export class StatisticsComponent implements OnInit {

  list$ = this.userService.getAll();
  chartData: ChartDataset[] = [];

  // Pie
  pieChartOptions: ChartOptions<'pie'> = {
    responsive: true,
  };
  pieChartLabels: any = [];
  pieChartDatasets: any = [ {
    data: [],
    backgroundColor: [],
    borderWidth: 5,
    borderColor: '#212121',
    hoverOffset: 10,
    borderRadius: 0,
  } ];
  pieChartLegend = true;
  pieChartPlugins = [];
  borderWidth = 0;
  loaded: boolean = false;

  faStar = faStar;

  constructor(
    private userService: UserService,
  ) { }

  ngOnInit(): void {
    AOS.init({
      duration: 600,
      offset: 130,
      easing: 'ease-in-sine'
    });

    this.list$.subscribe((data) => {
      data = data.slice(0,5);
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

}
