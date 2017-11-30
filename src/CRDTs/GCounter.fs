namespace CRDTs

type GCounter<'K> when 'K : comparison = { p: Map<'K, uint32> }

module GCounter =
  let increment (counter) (key) : GCounter<'K> =
    { p = counter.p }
    
  let value : (counter) : uint32 = 
    Map.fold counter.p

  let compare : GCounter<'K> -> GCounter<'K> -> bool
    true
  let merge : GCounter<'K> -> GCounter<'K> -> GCounter<'K>
   