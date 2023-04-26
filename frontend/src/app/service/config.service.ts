import { Injectable } from '@angular/core';

export interface ITableColumn {
  title: string;
  key: string;
  hidden?: boolean;
  outputTransform?: any;
  htmlOutput?: any;
  pipes?: any[];
  pipeArgs?: [any[]];
}

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  userTableColumns: ITableColumn[] = [
    { key: 'id', title: '#', pipes: [ConfigService.curveLongString], pipeArgs: [[0, 3]] },
    { key: 'username', title: 'Username' },
    { key: 'email', title: 'Email' },
    { key: 'joinDate', title: 'Join date' },
    { key: 'lastOnlineDate', title: 'Last online' },
    { key: 'scores', title: 'Games' },
    { key: 'coins', title: 'Coins' },
    { key: 'shopItems', title: 'Shop items' },
    { key: 'friends', title: 'Friends' },
    { key: 'role', title: 'Role' },
    { key: 'banned', title: 'Banned' },
  ];

  constructor() { }

  static curveLongString(
    data: string,
    start: number,
    end: number,
    curve: string = '...'
  ): string {
    if (data.length > end) {

      return data ? data.slice(start, end) + curve : data;
    }
    return data;
  }

}
