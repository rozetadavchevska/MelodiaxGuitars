import { Brand } from "./Brand"
import { Category } from "./Category"

export interface Product{
    id: string,
    name: string,
    description: string,
    brandId: string, 
    model: string,
    type: string,
    hand: string,
    bodyShape: string,
    color: string,
    top: string,
    sidesAndBack: string,
    neck: string,
    nut: string,
    fingerboard: string,
    strings: string,
    tuners: string,
    bridge: string,
    controls: string,
    pickups: string,
    pickupSwitch: string,
    cutaway: boolean, 
    pickguard: string,
    case: string,
    scaleLength: string,
    width: string,
    depth: string,
    weight: string,
    categoryId: string,
    imageUrl: string
}