
export interface RefreshToken {
  token: string;
  expires: Date;
}

export class RefreshTokenEntity implements RefreshToken{
  constructor() {
    this.token = '';
    this.expires = new Date();
  }

  token: string;
  expires: Date;
}
