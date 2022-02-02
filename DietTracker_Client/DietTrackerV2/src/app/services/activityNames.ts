export class ActivityNames{
  private static _instance: ActivityNames;

  public names: string[] = [ 'Running',
  'Swimming',
  'Hiking',
  'Walking',
  'Cycling'];

  constructor(){

  }

  public static get instance()
    {
        // Do you need arguments? Make it a regular static method instead.
        // eslint-disable-next-line no-underscore-dangle
        return this._instance || (this._instance = new this());
    }
}
