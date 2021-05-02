import {Component, Input, OnChanges, OnInit} from '@angular/core';
import {Animal} from '../../../models/animal';
import {ActivatedRoute} from '@angular/router';


@Component({
  selector: 'app-animal-list',
  templateUrl: './animal-list.component.html',
  styleUrls: ['./animal-list.component.css'],
})
export class AnimalListComponent implements OnInit, OnChanges {

  constructor(private route: ActivatedRoute) {
    this.animalsList = [];
    this.ownerId = 0;
    this.filteredAnimalList = [];
    this.activeFilterOn = false;
  }

  @Input() animalsList: Animal[];
  @Input() ownerId: number;
  filteredAnimalList: Animal[];
  activeFilterOn: boolean;

  filterAll(): void {
    this.activeFilterOn = false;
    this.filteredAnimalList = this.animalsList;
  }

  filterActive(): void {
    this.activeFilterOn = true;
    this.filteredAnimalList = this.animalsList.filter(
      animal => animal.active === true);
  }

  ngOnInit(): void {
    this.filterActive();
  }

  ngOnChanges(): void {
    this.filteredAnimalList = this.animalsList;
  }
}
