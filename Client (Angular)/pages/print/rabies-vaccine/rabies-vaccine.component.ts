import { Component, OnInit } from '@angular/core';
import {User, UserEntity} from '../../../models/user';
import {AnimalPageEntity} from '../../../models/animal-page';
import {PrintService} from '../print.service';
import {AnimalService} from '../../animal/animal.service';
import {AuthService} from '../../../auth/auth.service';
import {ActivatedRoute} from '@angular/router';
import {formatDate} from '@angular/common';

@Component({
  selector: 'app-rabies-vaccine',
  templateUrl: './rabies-vaccine.component.html',
  styleUrls: ['./rabies-vaccine.component.css']
})
export class RabiesVaccineComponent implements OnInit {

  constructor(private printService: PrintService, private animalService: AnimalService,
              private authService: AuthService, private route: ActivatedRoute) {
    this.animalId = Number(this.route.snapshot.paramMap.get('animalid'));
  }

  currentDate = new Date();
  reportH1Title = 'אישור חיסון כלבת';
  animalId: number;
  animalPage = new AnimalPageEntity();
  user: User = new UserEntity();

  ngOnInit(): void {
    this.user = this.authService.getUser();

    this.animalService.getAnimal(this.animalId).subscribe((animal) => {
      this.animalPage = animal;
    }, error => {}, () => {
      const currentDate = formatDate(new Date(), 'yyyy-MM-ddThh:mm:ss', 'he');
      this.printService.onDataReady('rabies-confirmation-' + currentDate);
    });
  }
}
