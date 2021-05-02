import { Pipe, PipeTransform } from '@angular/core';
import {AnimalType} from '../enums/animal-type';

@Pipe({name: 'animalImgUrlByType'})
export class AnimalImgUrlByTypePipe implements PipeTransform {
  transform(value: string): string {
    switch (value) {
      case AnimalType.Hamster: {
        return '../../assets/img/animals/hamster-100.jpg';
        break;
      }
      case AnimalType.Rabbit: {
        return '../../assets/img/animals/rabbit-100.jpg';
        break;
      }
      case AnimalType.Reptile: {
        return '../../assets/img/animals/reptile-100.jpg';
        break;
      }
      case AnimalType.Rat: {
        return '../../assets/img/animals/rat-100.jpg';
        break;
      }
      case AnimalType.Ferret: {
        return '../../assets/img/animals/ferret-100.jpg';
        break;
      }
      case AnimalType.GuineaPig: {
        return '../../assets/img/animals/guinea-pig-100.jpg';
        break;
      }
      case AnimalType.Pigeon: {
        return '../../assets/img/animals/placeholder-image.jpg';
        break;
      }
      case AnimalType.Bird: {
        return '../../assets/img/animals/bird-100.jpg';
        break;
      }
      case AnimalType.Parrot: {
        return '../../assets/img/animals/parrot-100.jpg';
        break;
      }
      case AnimalType.Cat: {
        return '../../assets/img/animals/cat-100.jpg';
        break;
      }
      case AnimalType.Dog: {
        return '../../assets/img/animals/dog-100.jpg';
        break;
      }

      case AnimalType.NotSet: {
        return '../../assets/img/animals/placeholder-image.jpg';
        break;
      }
      default: {
        return '../../assets/img/animals/placeholder-image.jpg';
        break;
      }
    }
  }
}
