export class ShopItem {
  _id: string = '';
  title: string = '';
  image: string = '';
  price: number = 0;
  color: string = '';  //  egy hex kód az item kártyájának a háttérszínéhez - ide jó lenne egy default szín
  description?: string = '';  // nem vagyok benne biztos, hogy szükséges
}
