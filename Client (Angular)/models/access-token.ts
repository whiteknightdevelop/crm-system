
export interface AccessToken {
  token: string;
  expires: Date;
}

export class AccessTokenEntity implements AccessToken{
  constructor() {
    this.token = '';
    this.expires = new Date();
  }
  token: string;
  expires: Date;
}
