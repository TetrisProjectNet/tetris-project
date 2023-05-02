import { ShopItem } from "./shop-item";

export class User {
  id: string = '';
  username?: string = '';
  email?: string = '';
  password?: string = '';
  role?: string = 'player';  // player / admin
  banned?: boolean = false;
  joinDate?: any = '';
  lastOnlineDate?: any = '';
  scores?: number[] = [];
  coins?: number | string = '';
  shopItems?: ShopItem[] = [];
  friends?: User[] = [];
  refreshToken?: string = '';
}
