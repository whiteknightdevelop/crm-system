import {Component, Input, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {Observable} from 'rxjs';
import { VisitListService } from './visit-list.service';
import { Visit } from '../../../models/visit';
import {Animal, AnimalEntity} from '../../../models/animal';
import {User, UserEntity} from '../../../models/user';

@Component({
  selector: 'app-visit-list',
  templateUrl: './visit-list.component.html',
  styleUrls: ['./visit-list.component.css']
})
export class VisitListComponent implements OnInit {

  constructor() {
    this.inputAnimal = new AnimalEntity();
    this.list = [];
    this.visits$ = new Observable<Visit[]>();
    this.visits = [];
    this.selectedId = 0;
    this.dialogVisible = false;
    this.currentUser = new UserEntity();
  }

  @Input() inputAnimal: Animal;
  @Input() list: Visit[];
  private visits$: Observable<Visit[]>;
  visits: Visit[];
  selectedId: number;
  dialogVisible: boolean;
  currentUser: User;

  ngOnInit(): void {
    this.visits = this.list;
  }

  showDialog(): void {
    this.visits$.subscribe(list => this.visits = list);
    this.dialogVisible = true;
  }
}
