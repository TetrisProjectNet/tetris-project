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
    { key: 'id', title: '#' },
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
}
