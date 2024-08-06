import { createSignal, createEffect } from 'solid-js';
import type { Component } from 'solid-js';
import { c } from 'vite/dist/node/types.d-aGj9QkWt';

const App: Component = () => {

  const [boardSize, setBoardSize] = createSignal(15);
  
  class Cell{
    isAlive: Boolean;
    numNeighbors: Number;
    
    constructor(isAlive: boolean, numNeighbors: number) {
      this.isAlive = isAlive;
      this.numNeighbors = numNeighbors;
    }
  }

  class Coordinate{
    x: Number;
    y: Number;
    cell: Cell;

    constructor(x: number, y: number, cell: Cell) {
      this.x = x;
      this.y = y;
      this.cell = cell;
    }
  }

  const [board, setBoard]= createSignal<Coordinate[]>([]);

  function HandleSize(size: number) {
    setBoardSize(size);
    CreateBoard(size);
  }

  function CreateBoard(size: number) {
    let newCell;
    let spot;
    let newCoords = [];
    for(let j = 0; j <size; j++)
    {
      for(let i = 0; i < size; i++)
      {
        newCell = new Cell(false, 0);
        spot = new Coordinate(j,i,newCell);
        newCoords.push(spot);
      }
    }
    setBoard(newCoords);
    
  };

  function HandleLife() {

  }

  createEffect(() => {
    console.log(board());
  })

  return (
    <div class="h-screen bg-orange-400">
      <div class="flex flex-col items-center">
        <h1>Conway's Game of Life</h1>
        <p>Board Size: {boardSize()}</p>
        <div class="flex flex-row space-x-4">
          <button class="border-solid border-2 border-black rounded-md bg-slate-500" onClick={(e) => HandleSize(10)}>Small</button>
          <button class="border-solid border-2 border-black rounded-md bg-slate-500" onClick={(e) => HandleSize(15)}>Medium</button>
          <button class="border-solid border-2 border-black rounded-md bg-slate-500" onClick={(e) => HandleSize(20)}>Large</button>
        </div>
      </div>
      <div class={`grid grid-cols-${boardSize()}`}>
        {board().map((coord, index) => (
          <div class={`border-solid border-2 border-black h-8 w-8 ${coord.cell.isAlive ? "bg-zinc-900" : "bg-zinc-500"}`}></div>
        ))}
      </div>
    </div>
  );
};

export default App;