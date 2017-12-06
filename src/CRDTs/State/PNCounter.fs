namespace CRDTs.State

type PNCounter<'K when 'K: comparison> = { positives: GCounter<'K>; negatives: GCounter<'K> }

module PNCounter =
  let init =
    { positives = GCounter.init; negatives = GCounter.init }
  let increment key counter =
    { positives = GCounter.increment key counter.positives; negatives = counter.negatives }
    
  let decrement key counter =
    { positives = counter.positives; negatives = GCounter.increment key counter.negatives }
    
  let query counter =
    (GCounter.query counter.positives) - (GCounter.query counter.negatives)

  let compare x y =
    (GCounter.compare x.positives y.positives) && (GCounter.compare x.negatives y.negatives)
    
  let merge x y =
    { positives = GCounter.merge x.positives y.positives; negatives = GCounter.merge x.negatives y.negatives }
