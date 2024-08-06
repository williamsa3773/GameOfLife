import { createSignal } from 'solid-js';
import type { Component } from 'solid-js';
import { c } from 'vite/dist/node/types.d-aGj9QkWt';

const App: Component = () => {
  const [boardSize, setBoardSize] = createSignal(15);
  const [grid, setGrid] = createSignal([]);

  class Cell {
    x: Number;
    y: Number;
    isAlive: Boolean;
    numNeighbors: Number;

    constructor(x: number, y: number, isAlive: boolean, numNeighbors: number){
      this.x = x;
      this.y = y;
      this.isAlive = isAlive;
      this.numNeighbors = numNeighbors;
    }
  }

  function HandleSize(size: number) {
    setBoardSize(size);
    CreateBoard(size);
  }

  function CreateBoard(size: number) {
    const newGrid: any = [];

    for(let j = 0; j <size; j++)
    {
      for(let i = 0; i < size; i++)
      {
        const newCell = new Cell(j,i,false,0)
        newGrid.push(newCell);
      }
    }

    setGrid(newGrid);
    console.log(newGrid);
  };

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
      <div class="">
        <div class="border-solid border-2 border-black h-8 w-8"></div>
      </div>
    </div>
  );
};

export default App;
