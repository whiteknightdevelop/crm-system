import {Component, OnInit} from '@angular/core';
import {User, UserEntity} from '../../../models/user';
import {AnimalService} from '../../animal/animal.service';
import {AnimalPageEntity} from '../../../models/animal-page';
import {AuthService} from '../../../auth/auth.service';
import {ActivatedRoute} from '@angular/router';
import {PrintService} from '../print.service';
import {formatDate} from '@angular/common';

@Component({
  selector: 'app-sterilization-permit',
  templateUrl: './sterilization-permit.component.html',
  styleUrls: ['./sterilization-permit.component.css']
})
export class SterilizationPermitComponent implements OnInit {

  constructor(private printService: PrintService, private animalService: AnimalService,
              private authService: AuthService, private route: ActivatedRoute) {
    this.animalId = Number(this.route.snapshot.paramMap.get('animalid'));
  }

  currentDate = new Date();
  reportH1Title = 'אישור וטרינרי לעיקור כלבים';
  animalId: number;
  animalPage = new AnimalPageEntity();
  user: User = new UserEntity();

  ngOnInit(): void {
    this.user = this.authService.getUser();

    this.animalService.getAnimal(this.animalId).subscribe((animal) => {
      this.animalPage = animal;
    }, error => {}, () => {
      const currentDate = formatDate(new Date(), 'yyyy-MM-ddThh:mm:ss', 'he');
      this.printService.onDataReady('sterilization-confirmation-' + currentDate);
    });
}
