namespace CRDTs

type GCounter<'K> when 'K : comparison = { p: Map<'K, uint32> }

module GCounter =
  val increment : GCounter<'K> -> 'K -> GCounter<'K>
    when 'K : comparison 
  val value : GCounter<'K> -> uint32
    when 'K : comparison 
  val compare : GCounter<'K> -> GCounter<'K> -> bool
    when 'K : comparison 
  val merge : GCounter<'K> -> GCounter<'K> -> GCounter<'K>
    when 'K : comparison 